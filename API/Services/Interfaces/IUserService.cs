using System;

namespace API.Services.Interfaces;

public interface IUserService
{
    Task<string> RegisterUserAsync (string userName, string password, string role, int filmStudioId );
    Task<string> AuthenticateUserAsync (string userName, string password);
}
