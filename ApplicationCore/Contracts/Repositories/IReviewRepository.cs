using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IReviewRepository: IRespository<Review>
{
    Task<Review> GetReviewByUser(int movieId, int userId);
    Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
}