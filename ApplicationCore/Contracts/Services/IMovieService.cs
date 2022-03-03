using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // have all the business logic methods relating to Movies
    List<MovieCardModel> GetTop30GrossingMovies();
}