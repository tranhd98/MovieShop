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
    public async Task< IEnumerable<Movie> >GetTop30RevenueMovies()
    {
        var movies = await _dbContext.Movie.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies; 
    }

    public async override Task<Movie> GetById(int id)
    {
        var movieDetails = await _dbContext.Movie
            .Include(m => m.Genres).ThenInclude(m => m.Genre)
            .Include(m => m.Trailers)
            .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
            .FirstOrDefaultAsync(m => m.Id == id);
        return movieDetails;
    }
}