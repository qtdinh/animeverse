using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AnimeVerse;

public partial class SeriesItem
{

    [Key]
    [Column("ID")]
    public int SeriesId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Demographic { get; set; } = null!;
    public int Year { get; set; }

    [InverseProperty("SeriesItem")]
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    [InverseProperty("Series")]
    public virtual ICollection<SeriesGenre> SeriesGenres { get; set; } = new List<SeriesGenre>();

}
