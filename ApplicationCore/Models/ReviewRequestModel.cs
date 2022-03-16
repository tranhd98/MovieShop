using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class ReviewRequestModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "Rating shouldn't be empty")]
    [Range(0.01, 9.99, ErrorMessage = "Rating should be in range 0.01 to 9.99")]
    public decimal Rating { get; set; }
    [Required(ErrorMessage = "Review Text shouldn't be empty")]
    public string ReviewText { get; set; }
}