using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IFavoriteRepository _favoriteRepository;


    public UserService(IReviewRepository reviewRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository)
    {
        _reviewRepository = reviewRepository;
        _purchaseRepository = purchaseRepository;
        _favoriteRepository = favoriteRepository;
    }

    public async Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
    {
        // check if user already purchased or not
        var purchasedByUser = await _purchaseRepository.GetPurchaseByUser(purchaseRequest.MovieId, userId);
        if (purchasedByUser != null)
        {
            throw new Exception("Already bought");
        }

        var newPurchased = new Purchase
        {
            UserId = userId,
            PurchaseDateTime = purchaseRequest.PurchaseDateTime,
            MovieId = purchaseRequest.MovieId,
            TotalPrice = purchaseRequest.TotalPrice,
            PurchaseNumber = purchaseRequest.PurchaseNumber
        };
        var createdPurchased = await _purchaseRepository.Add(newPurchased);
        return createdPurchased.Id;
    }

    public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        
        var purchasedByUser = await _purchaseRepository.GetPurchaseByUser(purchaseRequest.MovieId, userId);
        if (purchasedByUser != null)
        {
            return true;
        }

        return false;
    }

    public async Task<PurchaseModel> GetPurchasesDetails(int userId, int movieId)
    {
        var purchasedByUser = await _purchaseRepository.GetPurchaseByUser(movieId, userId);
        if (purchasedByUser == null)
        {
            return null;
        }
        return new PurchaseModel
        {
            Id = purchasedByUser.Id,
            UserId = purchasedByUser.UserId,
            PurchaseDateTime = purchasedByUser.PurchaseDateTime,
            TotalPrice = purchasedByUser.TotalPrice,
            PurchaseNumber = purchasedByUser.PurchaseNumber,
            Movie = new MovieCardModel
            {
                Id = purchasedByUser.Movie.Id,
                PosterURL = purchasedByUser.Movie.PosterUrl,
                Title = purchasedByUser.Movie.Title
            }
        };
        
    }

    public async Task<List<PurchaseModel>> GetAllPurchasesForUser(int id)
    {
        var Purchases = await _purchaseRepository.GetAllPurchasesForUser(id);
        var ListOfPurchase = new List<PurchaseModel>();
        foreach (var p in Purchases)
        {
            var PurchasesDetails = new PurchaseModel
            {
                Id = p.Id,
                UserId = p.UserId,
                PurchaseDateTime = p.PurchaseDateTime,
                TotalPrice = p.TotalPrice,
                PurchaseNumber = p.PurchaseNumber,
            };
            PurchasesDetails.Movie = new MovieCardModel
            {
                Id = p.MovieId,
                PosterURL = p.Movie.PosterUrl,
                Title = p.Movie.Title
            };

            ListOfPurchase.Add(PurchasesDetails);
        }

        return ListOfPurchase;
    }

    public async Task<int> AddFavorite(FavoriteRequestModel model)
    {
        var favorite = await _favoriteRepository.GetFavoriteByUser(model);
        if (favorite != null)
        {
            throw new Exception("already favorited");
        }

        var createdFavorite = await _favoriteRepository.Add(new Favorite
        {
            MovieId = model.MovieId,
            UserId = model.UserId
        });
        return createdFavorite.Id;
    }

    public async Task<int> RemoveFavorite(FavoriteRequestModel model)
    {
        var favorite = await _favoriteRepository.GetFavoriteByUser(model);
        if (favorite == null)
        {
            throw new Exception("not have that favorite");
        }

        var removedFavorite = await _favoriteRepository.Delete(favorite);
        return removedFavorite.Id;
    }

    public async Task<List<FavoriteModel>> GetAllFavoritesForUser(int id)
    {
        var favorites = await _favoriteRepository.GetAllFavoriteByUser(id);
        var newFavorites = new List<FavoriteModel>();
        foreach (var f in favorites)
        {
            newFavorites.Add(new FavoriteModel
            {
                Id = f.Id,
                MovieId = f.MovieId,
                UserId = f.UserId,
                Movie = new MovieCardModel
                {
                    Id = f.Movie.Id,
                    PosterURL = f.Movie.PosterUrl,
                    Title = f.Movie.Title,
                }
            });
            
        }

        return newFavorites;
    }

    public async Task<bool> FavoriteExists(int id, int movieId)
    {
        var favorite =
            await _favoriteRepository.GetFavoriteByUser(new FavoriteRequestModel {MovieId = movieId, UserId = id});
        if (favorite != null)
        {
            return true;
        }

        return false;
    }

    public async Task<ReviewModel> AddMovieReview(ReviewRequestModel reviewRequest)
    {
        var review = await _reviewRepository.GetReviewByUser(reviewRequest.MovieId, reviewRequest.UserId);
        if (review != null)
        {
            throw new Exception("already reviewed");
        }

        var createdReview = await _reviewRepository.Add(new Review
        {
            MovieId = reviewRequest.MovieId,
            UserId = reviewRequest.UserId,
            Rating = reviewRequest.Rating,
            ReviewText = reviewRequest.ReviewText
        });
        var reviewModel = new ReviewModel
        {
            MovieId = createdReview.MovieId,
            Rating = createdReview.Rating,
            ReviewText = createdReview.ReviewText,
            UserId = createdReview.UserId
        };
        return reviewModel;
    }

    public async Task<string> DeleteMovieReview(int movieId, int userId)
    {
        var review = await _reviewRepository.GetReviewByUser(movieId, userId);
        if (review == null)
        {
            throw new Exception("not reviewed yet");
        }

        var createdReview = await _reviewRepository.Delete(review);
        return "deleted";
    }

    public async Task<List<ReviewModel>> GetAllReviewsByUser(int id)
    {
        var reviews = await _reviewRepository.GetAllReviewsByUser(id);
        var reviewsModel = new List<ReviewModel>();
        foreach (var review in reviews)
        {
            reviewsModel.Add(new ReviewModel
            {
                MovieId = review.MovieId,
                UserId = review.UserId,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
            });
        }

        return reviewsModel;
    }

    public async Task<ReviewModel> UpdateMovieReview(ReviewRequestModel reviewRequest)
    {
        var review = await _reviewRepository.GetReviewByUser(reviewRequest.MovieId, reviewRequest.UserId);
        if (review == null)
        {
            throw new Exception("not reviewed yet");
        }

        review.ReviewText = reviewRequest.ReviewText;
        review.Rating = reviewRequest.Rating;
        var updatedReview = await _reviewRepository.Update(
            review
        );
        return new ReviewModel
        {
            MovieId = updatedReview.MovieId,
            Rating = updatedReview.Rating,
            ReviewText = updatedReview.ReviewText,
            UserId = updatedReview.UserId,
        };
    }

    public async Task<bool> isReviewExistByUser(int userId, int movieId)
    {
        var review = await _reviewRepository.GetReviewByUser(movieId, userId);
        if (review == null)
        {
            return false;
        }

        return true;
    }

    public async Task<ReviewModel> GetReviewsByUserAndMovie(int movieId, int userId)
    {
        var review = await _reviewRepository.GetReviewByUser(movieId, userId);
        return new ReviewModel
        {
            MovieId = review.MovieId,
            UserId = review.UserId,
            Rating = review.Rating,
            ReviewText = review.ReviewText,
        };
    }
}