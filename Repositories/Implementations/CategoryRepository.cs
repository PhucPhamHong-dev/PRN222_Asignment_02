using BusinessObjects.Models;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly CategoryDao _dao;

    public CategoryRepository(CategoryDao dao)
    {
        _dao = dao;
    }

    public Task<List<Category>> SearchAsync(string? keyword) => _dao.SearchAsync(keyword);
    public Task<List<Category>> GetActiveAsync() => _dao.GetActiveAsync();
    public Task<Category?> GetByIdAsync(short id) => _dao.GetByIdAsync(id);
    public Task<bool> IsUsedAsync(short id) => _dao.IsUsedAsync(id);
    public Task AddAsync(Category category) => _dao.AddAsync(category);
    public Task UpdateAsync(Category category) => _dao.UpdateAsync(category);
    public Task DeleteAsync(short id) => _dao.DeleteAsync(id);
}
