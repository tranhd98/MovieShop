namespace ApplicationCore.Models;

public class CastDetailsModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProfilePath { get; set; }
    public string Character { get; set; }
    public string TmdbUrl { get; set; }
    
    public ICollection<MovieDetailsModel> Movies { get; set; }
    
}