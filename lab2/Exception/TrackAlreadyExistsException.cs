namespace lab2.Exception;

public class TrackAlreadyExistsException(string trackName, string albumTitle)
    : System.Exception($"Track '{trackName}' already exists in album '{albumTitle}'.");