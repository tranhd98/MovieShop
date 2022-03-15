using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
    {
        // call MovieRespository(call the database with Dapper or EF Core
        var movies = await _movieRepository.GetTop30RevenueMovies();
        List<MovieCardModel> list = new List<MovieCardModel>();
        
        // mapping entities data into models data
        foreach (var movie in movies)
        {
            list.Add(new MovieCardModel
            {
                Id = movie.Id,
                PosterURL = movie.PosterUrl,
                Title = movie.Title
            });
        }
        return list;
    }

    public async Task<MovieDetailsModel> GetMovieDetails(int id)
    {
        var movie = await _movieRepository.GetById(id);
        MovieDetailsModel movieDetails = new MovieDetailsModel
        {
            Id = movie.Id, Price = movie.Price, Budget = movie.Budget, Overview = movie.Overview,
            Revenue = movie.Revenue, Tagline = movie.Tagline,
            Title = movie.Title, ImdbUrl = movie.ImdbUrl, RunTime = movie.RunTime, BackdropUrl = movie.BackdropUrl,
            PosterUrl = movie.PosterUrl,
            ReleaseDate = movie.ReleaseDate, TmdbUrl = movie.TmdbUrl
        };
        
        
        movieDetails.Genres = new List<GenreModel>();
        foreach (var genre in movie.Genres)
        {
            movieDetails.Genres.Add(new GenreModel
            {
                Id = genre.GenreId,
                Name = genre.Genre.Name,
            });
        }

        movieDetails.Casts = new List<CastModel>();
        foreach (var cast in movie.MovieCasts)
        {
            movieDetails.Casts.Add(new CastModel
            {
                Character = cast.Character,
                Id = cast.CastId,
                Name = cast.Cast.Name,
                ProfilePath = cast.Cast.ProfilePath
            });
        }

        movieDetails.Trailers = new List<TrailerModel>();
        foreach (var trailer in movie.Trailers)
        {
            movieDetails.Trailers.Add(new TrailerModel
            {
                Id = trailer.Id,
                Name = trailer.Name,
                TrailerUrl = trailer.TrailerUrl
            });
        }

        movieDetails.Reviews = new List<ReviewModel>();
        foreach (var review in movie.Reviews)
        {
            movieDetails.Reviews.Add(new ReviewModel
            {
                MovieId = review.MovieId,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                UserId = review.UserId
            });
        }

        return movieDetails;
    }

    public async Task<PagedResultSet<MovieCardModel>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1)
    {
        var pagedMovies = await _movieRepository.GetMoviesByGenres(genreId, pageSize, pageNumber);

        var moviesCard = new List<MovieCardModel>();

        foreach (var movie in pagedMovies.Data)
        {
            moviesCard.Add(new MovieCardModel
            {
                Id = movie.Id,
                PosterURL = movie.PosterUrl,
                Title = movie.Title
            });
        }

        var newPagedMovies = new PagedResultSet<MovieCardModel>(moviesCard, pageNumber, pageSize, pagedMovies.Count);
        return newPagedMovies;
    }
}