using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class MoviesController: Controller
{
    private readonly IMovieService _MovieService;

    public MoviesController(IMovieService movieService)
    {
        _MovieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await _MovieService.GetMovieDetails(id);
        return View(movieDetails);
    }

    public async Task<IActionResult> Genres(int id, int pageSize = 30, int pageNumber = 1)
    {
        var pagedNumbers = await _MovieService.GetMoviesByGenres(id, pageSize, pageNumber);
        return View("PagedNumbers", pagedNumbers);
    }
}