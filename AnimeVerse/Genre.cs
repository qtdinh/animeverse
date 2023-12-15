﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AnimeVerse;
public class Genre
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [InverseProperty("Genre")]
    public ICollection<SeriesGenre> SeriesGenres { get; set; } = new List<SeriesGenre>();
}