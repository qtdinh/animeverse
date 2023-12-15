namespace AnimeVerseAPI.DTOs
{
    public class CharacterDTO
    {
        public int CharacterId { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Series { get; set; } = null!;
    }
}
