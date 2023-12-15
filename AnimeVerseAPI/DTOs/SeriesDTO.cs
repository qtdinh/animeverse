using AnimeVerse;

namespace AnimeVerseAPI.DTOs
{
    public class SeriesDTO
    {
        public int SeriesId { get; set; }
        public string Title { get; set; } = null!;
        public string Demographic { get; set; } = null!;
        public int Year { get; set; }
        public IEnumerable<string> Genres { get; set; } = new List<string>();
    }
}
