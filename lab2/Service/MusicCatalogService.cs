using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using lab2.Entity;
using lab2.Exception;

namespace lab2.Service
{
    public class MusicCatalogService : IMusicCatalogService
    {
        private static MusicCatalogService? _instance;
        private static readonly object Lock = new();

        private const string FilePath = "catalog.json";
        public List<Artist> Artists { get; set; } = new();
        public List<Playlist> Playlists { get; set; } = new();

        private MusicCatalogService()
        {
            LoadFromFile();
        }

        public static MusicCatalogService Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ??= new MusicCatalogService();
                }
            }
        }

        private class MusicCatalog
        {
            public List<Artist> Artists { get; set; }
            public List<Playlist> Playlists { get; set; }
        }

        private void SaveToFile()
        {
            var catalog = new MusicCatalog
            {
                Artists = this.Artists,
                Playlists = this.Playlists
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            try
            {
                File.WriteAllText(FilePath, JsonSerializer.Serialize(catalog, options));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error saving catalog: {ex.Message}");
            }
        }

        private void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                try
                {
                    var catalog = JsonSerializer.Deserialize<MusicCatalog>(json, options);
                    this.Artists = catalog?.Artists ?? new List<Artist>();
                    this.Playlists = catalog?.Playlists ?? new List<Playlist>();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error loading catalog: {ex.Message}");
                }
            }
        }

        public void AddArtist(string name)
        {
            if (Artists.Any(a => a?.Name.Equals(name, StringComparison.OrdinalIgnoreCase) == true))
            {
                throw new ArtistAlreadyExistsException(name);
            }

            var artist = new Artist(name);
            Artists.Add(artist);
            SaveToFile();
        }

        public Album AddAlbumForArtist(string artistName, string title, DateTime releaseDate)
        {
            var artist = FindArtistByName(artistName);
            if (artist.Albums.Any(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new AlbumAlreadyExistsException(title, artistName);
            }

            var album = new Album(title, artist, releaseDate);
            artist.Albums.Add(album);
            SaveToFile();
            return album;
        }

        public Track AddTrackToAlbum(string artistName, string albumName, string trackName, TimeSpan duration, Genre genre)
        {
            var artist = FindArtistByName(artistName);
            var album = FindAlbumByArtist(artist, albumName);
            if (album.Tracks.Any(t => t.Title.Equals(trackName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new TrackAlreadyExistsException(trackName, albumName);
            }

            var track = new Track(trackName, duration, genre);
            album.Tracks.Add(track);
            SaveToFile();
            return track;
        }

        public Playlist AddPlaylist(string playlistName)
        {
            if (Playlists.Any(p => p?.Title.Equals(playlistName, StringComparison.OrdinalIgnoreCase) == true))
            {
                throw new PlaylistAlreadyExistsException(playlistName);
            }

            var playlist = new Playlist(playlistName);
            Playlists.Add(playlist);
            SaveToFile();
            return playlist;
        }

        public void AddTrackToPlaylist(string trackName, string artistName, string playlistName)
        {
            var track = FindTrackByNameAndArtist(trackName, artistName);
            var playlist = FindPlaylistByName(playlistName);
            if (playlist.Tracks.Any(t => t.Title.Equals(trackName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new TrackAlreadyExistsException(trackName, playlistName);
            }

            playlist.Tracks.Add(track);
            SaveToFile();
        }

        public Artist FindArtistByName(string name)
        {
            var artist = Artists.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (artist == null) throw new ArtistNotFoundException(name);
            return artist;
        }

        public Album FindAlbumByArtist(Artist artist, string title)
        {
            var album = artist.Albums.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (album == null) throw new AlbumNotFoundException(title);
            return album;
        }

        public Track FindTrackByNameAndArtist(string trackName, string artistName)
        {
            foreach (var artist in Artists)
            {
                if (artistName == artist?.Name)
                {
                    foreach (var album in artist.Albums)
                    {
                        var track = album.Tracks.FirstOrDefault(tr =>
                            tr.Title.Equals(trackName, StringComparison.OrdinalIgnoreCase));
                        if (track != null) return track;
                    }
                }
            }

            throw new ArgumentException($"Track '{trackName}' not found.");
        }

        public Playlist FindPlaylistByName(string playlistName)
        {
            var playlist = Playlists.FirstOrDefault(p => p.Title.Equals(playlistName, StringComparison.OrdinalIgnoreCase));
            if (playlist == null) throw new ArgumentException($"Playlist '{playlistName}' not found.");
            return playlist;
        }

        public string ShowAllArtists()
        {
            var result = new StringBuilder("All Artists:\n");
            foreach (var artist in Artists)
            {
                result.AppendLine(GetArtistDetails(artist));
            }

            return result.ToString();
        }

        public string ShowAllPlaylists()
        {
            var result = new StringBuilder("\nAll Playlists:\n");
            foreach (var playlist in Playlists)
            {
                result.AppendLine(playlist?.Title);
                int i = 1;
                foreach (var track in playlist?.Tracks!)
                {
                    result.AppendLine($"    {i++}. {track.Title} - Duration: {track.Duration}");
                }
            }

            return result.ToString();
        }

        private string GetArtistDetails(Artist? artist)
        {
            var result = new StringBuilder($"Artist: {artist?.Name}\nAlbums:\n");
            foreach (var album in artist?.Albums!)
            {
                result.AppendLine($"  {album.Title} - Release Date: {album.ReleaseDate}");
                int i = 1;
                foreach (var track in album.Tracks)
                {
                    result.AppendLine($"    {i++}. {track.Title} - Duration: {track.Duration}");
                }
            }

            return result.ToString();
        }
    }
}
