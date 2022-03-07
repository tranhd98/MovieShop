using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;
[Table("Genre")]
public class Genre
{
    public int Id { get; set; }
    [MaxLength(64)]
    public string Name { get; set; }
    // navigatinon property
    public ICollection<MovieGenre> Movies { get; set; }
    
}