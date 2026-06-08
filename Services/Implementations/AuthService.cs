using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IAccountService _accountService;

    public AuthService(IConfiguration configuration, IAccountService accountService)
    {
        _configuration = configuration;
        _accountService = accountService;
    }

    public async Task<(bool Success, string Role, string Name, string AccountId)> AuthenticateAsync(string email, string password)
    {
        var adminEmail = _configuration["AdminAccount:Email"];
        var adminPassword = _configuration["AdminAccount:Password"];
        if (email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && password == adminPassword)
        {
            return (true, AppRoles.Admin, "Administrator", "0");
        }

        var account = await _accountService.GetByEmailAsync(email);
        if (account is null || account.AccountPassword != password)
        {
            return (false, string.Empty, string.Empty, string.Empty);
        }

        return (true, AppRoles.ToName(account.AccountRole), account.AccountName, account.AccountId.ToString());
    }
}
