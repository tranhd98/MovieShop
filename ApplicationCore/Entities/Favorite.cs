namespace ApplicationCore.Entities;

public class Favorite
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    //navigation fields
    public Movie Movie { get; set; }
    public User User { get; set; }
}