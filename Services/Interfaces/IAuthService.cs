using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string Role, string Name, string AccountId)> AuthenticateAsync(string email, string password);
}
