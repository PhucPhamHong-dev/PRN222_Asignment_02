using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _repository;

    public NewsArticleService(INewsArticleRepository repository)
    {
        _repository = repository;
    }

    public Task<List<NewsArticle>> SearchAsync(string? keyword, bool activeOnly = false, short? createdById = null) => _repository.SearchAsync(keyword, activeOnly, createdById);
    public Task<List<NewsArticle>> ReportAsync(DateTime startDate, DateTime endDate) => _repository.ReportAsync(startDate, endDate);
    public Task<NewsArticle?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
    public Task AddAsync(NewsArticle article, string tagList) => _repository.AddAsync(article, tagList);
    public Task UpdateAsync(NewsArticle article, string tagList) => _repository.UpdateAsync(article, tagList);
    public Task DeleteAsync(string id) => _repository.DeleteAsync(id);
}
