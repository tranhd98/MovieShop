using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        if (user != null)
        {
            // create an authentication cookie and store some claims information in the cookie
            
            
            //1st step create claim object to store user claims info
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim("Full Name", user.FirstName + " " + user.LastName),
                new Claim("Language", "en")

            };
            // 2nd step identity object. Think as driver license to show all claims.
            var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            // 3rd step
            // create the cookie
            // SignInAsync
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));
            
            return LocalRedirect("~/");
        }

        return View();
    }
    
}