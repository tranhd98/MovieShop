using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CastService: ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<CastDetailsModel> GetCastDetails(int id)
    {
        var cast = await _castRepository.GetById(id);
        var castDetails = new CastDetailsModel
        {
            Id = cast.Id,
            Name = cast.Name,
            ProfilePath = cast.ProfilePath,
            TmdbUrl = cast.TmdbUrl
        };
        castDetails.Movies = new List<MovieDetailsModel>();
        foreach (var movie in cast.MovieCasts)
        {
            castDetails.Movies.Add(new MovieDetailsModel
            {
                Id = movie.MovieId, Price = movie.Movie.Price, Budget = movie.Movie.Budget, Overview = movie.Movie.Overview,
                Revenue = movie.Movie.Revenue, Tagline = movie.Movie.Tagline,
                Title = movie.Movie.Title, ImdbUrl = movie.Movie.ImdbUrl, RunTime = movie.Movie.RunTime, BackdropUrl = movie.Movie.BackdropUrl,
                PosterUrl = movie.Movie.PosterUrl,
                ReleaseDate = movie.Movie.ReleaseDate, TmdbUrl = movie.Movie.TmdbUrl
            });
        }

        return castDetails;
    }
}