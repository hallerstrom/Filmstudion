using System;
using API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using API.Services.Interfaces;
namespace API.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Registrera användare med IdentityCore
        public async Task<string> RegisterUserAsync(string username, string password, string role, int filmStudioId)
        {
            var user = new User
            {
                UserName = username,
                Role = role,
                FilmStudioId = filmStudioId
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Tilldela användare en roll
                await _userManager.AddToRoleAsync(user, role);
                return user.Id;
            }
            return null; // Vad ska returneras här?
        }

        // Autentisera användare med IdentityCore
        public async Task<string> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (result.Succeeded)
            {
                return null; // Vill returnera JWT-token
            }

            return null;
        }

}
