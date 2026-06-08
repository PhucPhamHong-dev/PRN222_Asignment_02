using BusinessObjects.Models;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDao _dao;

    public AccountRepository(AccountDao dao)
    {
        _dao = dao;
    }

    public Task<List<SystemAccount>> SearchAsync(string? keyword) => _dao.SearchAsync(keyword);
    public Task<SystemAccount?> GetByIdAsync(short id) => _dao.GetByIdAsync(id);
    public Task<SystemAccount?> GetByEmailAsync(string email) => _dao.GetByEmailAsync(email);
    public Task AddAsync(SystemAccount account) => _dao.AddAsync(account);
    public Task UpdateAsync(SystemAccount account) => _dao.UpdateAsync(account);
    public Task DeleteAsync(short id) => _dao.DeleteAsync(id);
}
