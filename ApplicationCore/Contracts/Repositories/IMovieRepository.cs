using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository: IRespository<Movie>
{
    IEnumerable<Movie> GetTop30RevenueMovies();
    
}