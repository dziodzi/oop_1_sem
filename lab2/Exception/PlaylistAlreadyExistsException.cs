namespace lab2.Exception;

public class PlaylistAlreadyExistsException(string playlistName)
    : System.Exception($"Playlist '{playlistName}' already exists.");