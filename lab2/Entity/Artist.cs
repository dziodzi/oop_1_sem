namespace lab2.Entity
{
    public class Artist
    {
        public string Name { get; set; }
        public List<Album> Albums { get; set; }

        public Artist() {}

        public Artist(string name)
        {
            Name = name;
            Albums = new List<Album>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}