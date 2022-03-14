using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{

    Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
    Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);

    Task<PurchaseModel> GetPurchasesDetails(int userId, int movieId);

    Task<List<PurchaseModel>> GetAllPurchasesForUser(int id);

    Task<int> AddFavorite(FavoriteRequestModel model);

    Task<int> RemoveFavorite(FavoriteRequestModel model);

    Task<List<FavoriteModel>> GetAllFavoritesForUser(int id);

    Task<bool> FavoriteExists(int id, int movieId);

    Task<ReviewModel> AddMovieReview(ReviewRequestModel reviewRequest);
    Task<string> DeleteMovieReview(int userId, int movieId);

    Task<List<ReviewModel>> GetAllReviewsByUser(int id);

    Task<ReviewModel> UpdateMovieReview(ReviewRequestModel reviewRequest);
}