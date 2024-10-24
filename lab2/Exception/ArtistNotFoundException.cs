namespace lab2.Exception;

public class ArtistNotFoundException(string artistName) : System.Exception($"Artist '{artistName}' not found.");