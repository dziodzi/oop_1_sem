namespace lab2.Exception
{
    public class ArtistAlreadyExistsException(string artistName)
        : System.Exception($"Artist '{artistName}' already exists.");
}