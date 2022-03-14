namespace ApplicationCore.Models;

public class PurchaseRequestModel
{
    public int MovieId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public System.Guid? PurchaseNumber { get; set; }
}