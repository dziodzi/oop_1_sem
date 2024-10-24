using System.Text;
using lab2.Entity;
using lab2.Exception;
using lab2.Service;

namespace lab2.Controller
{
    public class MusicCatalogController : IMusicCatalogController
    {
        private readonly MusicCatalogService _musicCatalog;
        private readonly SearchService _searchService;

        public MusicCatalogController()
        {
            _musicCatalog = MusicCatalogService.Instance;
            _searchService = new SearchService(_musicCatalog);
        }

        public string Search(string searchType, string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchType) || string.IsNullOrWhiteSpace(searchQuery))
            {
                return "Invalid request.";
            }

            try
            {
                return searchType switch
                {
                    "Artist" => GetArtistInfo(searchQuery),
                    "Album" => GetAlbumInfo(searchQuery),
                    "Track" => GetTrackInfo(searchQuery),
                    "Playlist" => GetPlaylistInfo(searchQuery),
                    _ => "Invalid selection."
                };
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public string ShowAll()
        {
            var result = new StringBuilder();
            result.AppendLine(_musicCatalog.ShowAllArtists());
            result.AppendLine(_musicCatalog.ShowAllPlaylists());
            return result.ToString();
        }

        public string CreateArtist(string name)
        {
            try
            {
                _musicCatalog.AddArtist(name);
                return $"Artist '{name}' added.";
            }
            catch (ArtistAlreadyExistsException ex)
            {
                return ex.Message;
            }
        }

        public string CreateAlbumForArtist(string artistName, string title, DateTime releaseDate)
        {
            try
            {
                Album album = _musicCatalog.AddAlbumForArtist(artistName, title, releaseDate);
                return $"Album '{album.Title}' added to artist '{artistName}'.";
            }
            catch (ArtistNotFoundException ex)
            {
                return ex.Message;
            }
            catch (AlbumAlreadyExistsException ex)
            {
                return ex.Message;
            }
        }

        public string CreateTrackToAlbum(string artistName, string albumName, string trackName, TimeSpan duration,
            Genre genre)
        {
            try
            {
                Track track = _musicCatalog.AddTrackToAlbum(artistName, albumName, trackName, duration, genre);
                return $"Track '{track.Title}' added to album '{albumName}' by artist '{artistName}'.";
            }
            catch (ArtistNotFoundException ex)
            {
                return ex.Message;
            }
            catch (AlbumNotFoundException ex)
            {
                return ex.Message;
            }
            catch (TrackAlreadyExistsException ex)
            {
                return ex.Message;
            }
        }

        public string CreatePlaylist(string playlistName)
        {
            try
            {
                Playlist playlist = _musicCatalog.AddPlaylist(playlistName);
                return $"Playlist '{playlist.Title}' created.";
            }
            catch (PlaylistAlreadyExistsException ex)
            {
                return ex.Message;
            }
        }

        public string CreateTrackToPlaylist(string trackName, string artistName, string playlistName)
        {
            try
            {
                _musicCatalog.AddTrackToPlaylist(trackName, artistName, playlistName);
                return $"Track '{trackName}' added to playlist '{playlistName}'.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            catch (TrackAlreadyExistsException ex)
            {
                return ex.Message;
            }
        }

        public string GetArtistInfo(string name)
        {
            try
            {
                var artist = _searchService.SearchArtistByName(name);
                return GetArtistDetails(artist);
            }
            catch (ArtistNotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string GetAlbumInfo(string title)
        {
            try
            {
                var album = _searchService.SearchAlbumByTitle(title);
                return GetAlbumDetails(album);
            }
            catch (AlbumNotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string GetTrackInfo(string title)
        {
            try
            {
                var track = _searchService.SearchTrackByTitle(title);
                return GetTrackDetails(track);
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
        }

        public string GetPlaylistInfo(string title)
        {
            try
            {
                var playlist = _searchService.SearchPlaylistByTitle(title);
                return playlist != null ? GetPlaylistDetails(playlist) : "Playlist not found.";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }



        private string GetArtistDetails(Artist? artist)
        {
            if (artist == null)
            {
                return "Artist not found.";
            }

            var result = new StringBuilder($"Artist: {artist.Name}\nAlbums:\n");
            foreach (var album in artist.Albums)
            {
                result.AppendLine($"  {album.Title} - Release Date: {album.ReleaseDate}");
            }

            return result.ToString();
        }
        
        private string GetTrackDetails(Track track)
        {
            var result = new StringBuilder(
                $"Track: {track.Title}\nGenre: {track.Genre}\nDuration: {track.Duration:mm\\:ss}\n");

            var album = _searchService.FindAlbumByTrack(track);
            result.AppendLine($"Album: {album.Title}\nArtist: {album.Artist.Name}\n");

            var playlists = _searchService.FindPlaylistsByTrack(track);
            if (playlists.Any())
            {
                result.AppendLine("Playlists:");
                foreach (var playlist in playlists)
                {
                    result.AppendLine($"  {playlist.Title}");
                }
            }

            return result.ToString();
        }

        private string GetAlbumDetails(Album album)
        {
            var result = new StringBuilder($"Album: {album.Title} - Release Date: {album.ReleaseDate}\nTracks:\n");
            foreach (var track in album.Tracks)
            {
                result.AppendLine($"  {track.Title} - {track.Duration:mm\\:ss}");
            }

            return result.ToString();
        }

        private string GetPlaylistDetails(Playlist playlist)
        {
            var result = new StringBuilder($"Playlist: {playlist.Title}\nTracks:\n");
            foreach (var track in playlist.Tracks)
            {
                var album = _searchService.FindAlbumByTrack(track);
                result.AppendLine(
                    $"  {track.Title} - {track.Duration:mm\\:ss} [{track.Genre}] (Artist: {album.Artist.Name})");
            }

            return result.ToString();
        }
    }
}