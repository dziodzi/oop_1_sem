namespace lab2.Exception;

public class AlbumNotFoundException(string albumTitle) : System.Exception($"Album '{albumTitle}' not found.");