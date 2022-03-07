﻿using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{

    private readonly IMovieService _movieService;

    public HomeController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var movies = _movieService.GetTop30GrossingMovies();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}