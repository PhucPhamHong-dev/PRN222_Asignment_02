using BusinessObjects.Models;

namespace Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> SearchAsync(string? keyword);
    Task<List<Category>> GetActiveAsync();
    Task<Category?> GetByIdAsync(short id);
    Task<(bool Success, string Message)> DeleteAsync(short id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
}
