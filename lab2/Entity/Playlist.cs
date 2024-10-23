namespace lab2.Entity
{
    public class Playlist
    {
        public string Title { get; set; }
        public List<Track> Tracks { get; set; }

        public Playlist() {}

        public Playlist(string title)
        {
            Title = title;
            Tracks = new List<Track>();
        }
    }
}