using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IAccountService
{
    Task<List<SystemAccount>> SearchAsync(string? keyword);
    Task<SystemAccount?> GetByIdAsync(short id);
    Task<SystemAccount?> GetByEmailAsync(string email);
    Task AddAsync(SystemAccount account);
    Task UpdateAsync(SystemAccount account);
    Task DeleteAsync(short id);
}
