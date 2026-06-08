using BusinessObjects.Models;

namespace Services.Interfaces;

public interface INewsArticleService
{
    Task<List<NewsArticle>> SearchAsync(string? keyword, bool activeOnly = false, short? createdById = null);
    Task<List<NewsArticle>> ReportAsync(DateTime startDate, DateTime endDate);
    Task<NewsArticle?> GetByIdAsync(string id);
    Task AddAsync(NewsArticle article, string tagList);
    Task UpdateAsync(NewsArticle article, string tagList);
    Task DeleteAsync(string id);
}
