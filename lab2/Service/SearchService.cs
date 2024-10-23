using lab2.Entity;
using lab2.Exception;

namespace lab2.Service
{
    public class SearchService(MusicCatalogService catalogService)
    {
        public Artist SearchArtistByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Artist name cannot be null or empty", nameof(name));

            var artist = catalogService.Artists
                .FirstOrDefault(a => a.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);

            if (artist == null)
                throw new ArtistNotFoundException(name);

            return artist;
        }

        public Album SearchAlbumByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) 
                throw new ArgumentException("Album title cannot be null or empty", nameof(title));

            var album = catalogService.Artists
                .SelectMany(a => a.Albums)
                .FirstOrDefault(al => al.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);

            if (album == null)
                throw new AlbumNotFoundException(title);

            return album;
        }

        public Track SearchTrackByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) 
                throw new ArgumentException("Track title cannot be null or empty", nameof(title));

            var track = catalogService.Artists
                .SelectMany(a => a.Albums)
                .SelectMany(al => al.Tracks)
                .Union(catalogService.Playlists.SelectMany(p => p.Tracks))
                .FirstOrDefault(t => t.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);

            if (track == null)
                throw new ArgumentException($"Track with title '{title}' not found.");

            return track;
        }

        public Playlist? SearchPlaylistByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) 
                throw new ArgumentException("Playlist title cannot be null or empty", nameof(title));

            return catalogService.Playlists
                .FirstOrDefault(p => p.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public Album FindAlbumByTrack(Track track)
        {
            if (track == null)
                throw new ArgumentNullException(nameof(track));

            var album = catalogService.Artists
                .SelectMany(a => a.Albums)
                .FirstOrDefault(al => al.Tracks.Contains(track));

            if (album == null)
                throw new AlbumNotFoundException($"Track '{track.Title}'");

            return album;
        }

        public List<Playlist?> FindPlaylistsByTrack(Track track)
        {
            if (track == null)
                throw new ArgumentNullException(nameof(track));

            return catalogService.Playlists
                .Where(p => p.Tracks.Contains(track))
                .ToList();
        }
    }
}
