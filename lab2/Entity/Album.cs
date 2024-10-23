namespace lab2.Entity
{
    public class Album
    {
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public List<Track> Tracks { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Album() {}

        public Album(string title, Artist artist, DateTime releaseDate)
        {
            Title = title;
            Artist = artist;
            Tracks = new List<Track>();
            ReleaseDate = releaseDate;
        }
    }
}