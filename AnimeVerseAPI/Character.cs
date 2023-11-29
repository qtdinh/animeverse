using System;
using System.Collections.Generic;

namespace AnimeVerse;

public partial class Character
{
    public int CharacterId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int SeriesId { get; set; }

    public virtual Series CharacterNavigation { get; set; } = null!;
}
