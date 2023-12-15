namespace AnimeVerseAPI.Data
{
    public class AnimeVerseCsv
    {
        public string seriesItem { get; set; } = null!;
        public string character { get; set; } = null!;
        public string gender { get; set; } = null!;
        public int year { get; set; }
        public int? age { get; set; }
        public string genres { get; set; } = null!;
        public string demographic { get; set; } = null!;
        public long id { get; set; }
    }
}
