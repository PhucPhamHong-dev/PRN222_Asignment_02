using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Category>> SearchAsync(string? keyword) => _repository.SearchAsync(keyword);
    public Task<List<Category>> GetActiveAsync() => _repository.GetActiveAsync();
    public Task<Category?> GetByIdAsync(short id) => _repository.GetByIdAsync(id);
    public Task AddAsync(Category category) => _repository.AddAsync(category);
    public Task UpdateAsync(Category category) => _repository.UpdateAsync(category);

    public async Task<(bool Success, string Message)> DeleteAsync(short id)
    {
        if (await _repository.IsUsedAsync(id))
        {
            return (false, "This category is already used by a news article and cannot be deleted.");
        }

        await _repository.DeleteAsync(id);
        return (true, "Category deleted successfully.");
    }
}
