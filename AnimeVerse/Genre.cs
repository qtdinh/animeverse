using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Genre
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }
}