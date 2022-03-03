using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService: IMovieService
{
    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        // call MovieRespository(call the database with Dapper or EF Core
        var movies = new List<MovieCardModel>()
        {
            new MovieCardModel {Id = 1, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 2, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 3, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 4, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 5, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 6, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 7, PosterURL = "", Title = ""},
            new MovieCardModel {Id = 8, PosterURL = "", Title = ""},
        };
        return movies;
    }
}