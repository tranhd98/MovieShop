using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class AccountController: Controller
{
    private readonly IAccountService _accountService;


    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterRequestModel model)
    {
        var user = await _accountService.CreateUser(model);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestModel model)
    {
        var user = await _accountService.ValidateUser(model);
        if (user)
        {
            return LocalRedirect("~/");
        }

        return View();
    }
    
}