namespace ApplicationCore.Entities;

public class Purchase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public System.Guid? PurchaseNumber { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? PurchaseDateTime { get; set; }
    public int MovieId { get; set; }
    // navigation fields
    public User User { get; set; }
    public Movie Movie { get; set; }
}