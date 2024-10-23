using lab2.Entity;

namespace lab2.Service
{
    public interface IMusicCatalogService
    {
        
        void AddArtist(string name);
        Album AddAlbumForArtist(string artistName, string title, DateTime releaseDate);
        Track AddTrackToAlbum(string artistName, string albumName, string trackName, TimeSpan duration, Genre genre);
        Playlist AddPlaylist(string playlistName);
        void AddTrackToPlaylist(string trackName, string artistName, string playlistName);
        
        string ShowAllArtists();
        string ShowAllPlaylists();
        
        Artist FindArtistByName(string name);
        Album FindAlbumByArtist(Artist artist, string title);
        Track FindTrackByNameAndArtist(string trackName, string artistName);
        Playlist FindPlaylistByName(string playlistName);
    }
}