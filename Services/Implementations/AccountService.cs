using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public Task<List<SystemAccount>> SearchAsync(string? keyword) => _repository.SearchAsync(keyword);
    public Task<SystemAccount?> GetByIdAsync(short id) => _repository.GetByIdAsync(id);
    public Task<SystemAccount?> GetByEmailAsync(string email) => _repository.GetByEmailAsync(email);
    public Task AddAsync(SystemAccount account) => _repository.AddAsync(account);
    public Task UpdateAsync(SystemAccount account) => _repository.UpdateAsync(account);
    public Task DeleteAsync(short id) => _repository.DeleteAsync(id);
}
