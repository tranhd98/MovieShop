namespace ApplicationCore.Models;

public class ReviewRequestModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; }
}