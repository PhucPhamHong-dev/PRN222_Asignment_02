using BusinessObjects.Models;
using DataAccessObjects.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public class CategoryDao
{
    private readonly IDbContextFactory<FunewsDbContext> _contextFactory;

    public CategoryDao(IDbContextFactory<FunewsDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Category>> SearchAsync(string? keyword)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var query = context.Categories.Include(category => category.ParentCategory).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(category => category.CategoryName.Contains(keyword) ||
                                            (category.CategoryDescription != null && category.CategoryDescription.Contains(keyword)));
        }

        return await query.OrderBy(category => category.CategoryName).ToListAsync();
    }

    public async Task<List<Category>> GetActiveAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Categories.AsNoTracking()
            .Where(category => category.CategoryStatus)
            .OrderBy(category => category.CategoryName)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(short id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.CategoryId == id);
    }

    public async Task<bool> IsUsedAsync(short id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.NewsArticles.AnyAsync(article => article.CategoryId == id);
    }

    public async Task AddAsync(Category category)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        category.CategoryDescription ??= string.Empty;
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        category.CategoryDescription ??= string.Empty;
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(short id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var category = await context.Categories.FindAsync(id);
        if (category is null)
        {
            return;
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
}
