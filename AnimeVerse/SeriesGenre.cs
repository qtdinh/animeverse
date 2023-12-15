using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AnimeVerse;

    public partial class SeriesGenre
    {
        public int SeriesId { get; set; }
        public SeriesItem Series { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
 
    }

