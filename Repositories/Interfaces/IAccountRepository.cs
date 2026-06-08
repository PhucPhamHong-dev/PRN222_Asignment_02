using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IAccountRepository
{
    Task<List<SystemAccount>> SearchAsync(string? keyword);
    Task<SystemAccount?> GetByIdAsync(short id);
    Task<SystemAccount?> GetByEmailAsync(string email);
    Task AddAsync(SystemAccount account);
    Task UpdateAsync(SystemAccount account);
    Task DeleteAsync(short id);
}
