namespace lab2.Exception;

public abstract class TrackNotFoundException(string albumTitle) : System.Exception($"Track '{albumTitle}' not found.");
