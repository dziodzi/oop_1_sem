namespace lab2.Entity
{
    public class Track
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public Genre Genre { get; set; }

        public Track() {}

        public Track(string title, TimeSpan duration, Genre genre)
        {
            Title = title;
            Duration = duration;
            Genre = genre;
        }
    }
}