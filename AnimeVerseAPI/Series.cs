using System;
using System.Collections.Generic;

namespace AnimeVerse;

public partial class Series
{
    public int SeriesId { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int Year { get; set; }

    public virtual Character? Character { get; set; }
}
