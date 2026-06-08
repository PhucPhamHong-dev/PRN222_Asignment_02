using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface INewsArticleRepository
{
    Task<List<NewsArticle>> SearchAsync(string? keyword, bool activeOnly = false, short? createdById = null);
    Task<List<NewsArticle>> ReportAsync(DateTime startDate, DateTime endDate);
    Task<NewsArticle?> GetByIdAsync(string id);
    Task AddAsync(NewsArticle article, string tagList);
    Task UpdateAsync(NewsArticle article, string tagList);
    Task DeleteAsync(string id);
}
