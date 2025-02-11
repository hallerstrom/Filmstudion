using System;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

//Service för autentisering och användarhantering
public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    // Konstruktor
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Registrera användare (filmstudio eller admin)
    public async Task<User> RegisterUserAsync(string username, string password, string role, int? filmstudioId = null )
    {
        var user = new User { UserName = username, Role = role, FilmStudioId = filmstudioId?? 0 };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return user;
        }
        else
        {
            throw new Exception("User registration failed");
        }
    }

    // Autentisera användare
    public async Task<User?> AuthenticateUserAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

        return result.Succeeded ? await _userManager.FindByNameAsync(username): null;

    }
}
