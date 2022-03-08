using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository: IRespository<Movie>
{
    Task<IEnumerable<Movie>> GetTop30RevenueMovies();
    
}