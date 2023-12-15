using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
namespace AnimeVerse;

public partial class Character
{

    [Key]
    [Column("ID")]
    public int CharacterId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    public int SeriesId { get; set; }

    [StringLength(50)]
    [Unicode(false)]

    [ForeignKey("SeriesId")]
    [InverseProperty("Characters")]
    public virtual SeriesItem SeriesItem { get; set; } = null!;
}
