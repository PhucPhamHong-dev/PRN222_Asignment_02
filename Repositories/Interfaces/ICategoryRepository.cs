using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> SearchAsync(string? keyword);
    Task<List<Category>> GetActiveAsync();
    Task<Category?> GetByIdAsync(short id);
    Task<bool> IsUsedAsync(short id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(short id);
}
