using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
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

    public async Task<PagedResultSet<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1)
    {
        var count = await _dbContext.MovieGenres.Where(mg => mg.GenreId == genreId).CountAsync();
        if (count == 0)
        {
            throw new Exception("None movies existed");
        }

        var movies = await _dbContext.MovieGenres
            .Where(mg => mg.GenreId == genreId)
            .Include(mg => mg.Movie)
            .OrderBy(mg => mg.MovieId)
            .Select(mg => new Movie
            {
                Id = mg.MovieId,
                PosterUrl = mg.Movie.PosterUrl,
                Title = mg.Movie.Title
            })
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, count);
        return pagedMovies;
    }

    public async override Task<Movie> GetById(int id)
    {
        var movieDetails = await _dbContext.Movie
            .Include(m => m.Genres).ThenInclude(m => m.Genre)
            .Include(m => m.Trailers)
            .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
            .Include(m=> m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);
        return movieDetails;
    }
}