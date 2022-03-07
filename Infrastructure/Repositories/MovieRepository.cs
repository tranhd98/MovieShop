using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository: EfRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }
    public IEnumerable<Movie> GetTop30RevenueMovies()
    {
        var movies = _dbContext.Movie.OrderByDescending(m => m.Revenue).Take(30);
        return movies; 
    }

    public override Movie GetById(int id)
    {
        var movieDetails = _dbContext.Movie
            .Include(m => m.Genres).ThenInclude(m => m.Genre)
            .Include(m => m.Trailers)
            .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
            .FirstOrDefault(m => m.Id == id);
        return movieDetails;
    }
}