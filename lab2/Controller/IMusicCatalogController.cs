using lab2.Entity;

namespace lab2.Controller
{
    public interface IMusicCatalogController
    {
        string CreateArtist(string name);
        string CreateAlbumForArtist(string artistName, string title, DateTime releaseDate);
        string CreateTrackToAlbum(string artistName, string albumName, string trackName, TimeSpan duration, Genre genre);
        string CreatePlaylist(string playlistName);
        string CreateTrackToPlaylist(string trackName, string artistName, string playlistName);
        
        string Search(string searchType, string searchQuery);
        string ShowAll();
        
        string GetArtistInfo(string name);
        string GetAlbumInfo(string title);
        string GetTrackInfo(string title);
        string GetPlaylistInfo(string title);
    }
}