namespace AnimeVerseAPI.Data
{
    public class AnimeVerseCSV
    {
        public string series { get; set; } = null!;
        public string character { get; set; } = null!;
        public string gender { get; set; } = null!;
        public int year { get; set; }
        public int? age { get; set; }
        public List<string> genres { get; set; } = new List<string>();
        public string demographic { get; set; } = null!;
        public long id { get; set; }
    }
}
