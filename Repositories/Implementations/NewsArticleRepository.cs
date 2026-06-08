using BusinessObjects.Models;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class NewsArticleRepository : INewsArticleRepository
{
    private readonly NewsArticleDao _dao;

    public NewsArticleRepository(NewsArticleDao dao)
    {
        _dao = dao;
    }

    public Task<List<NewsArticle>> SearchAsync(string? keyword, bool activeOnly = false, short? createdById = null) => _dao.SearchAsync(keyword, activeOnly, createdById);
    public Task<List<NewsArticle>> ReportAsync(DateTime startDate, DateTime endDate) => _dao.ReportAsync(startDate, endDate);
    public Task<NewsArticle?> GetByIdAsync(string id) => _dao.GetByIdAsync(id);
    public Task AddAsync(NewsArticle article, string tagList) => _dao.AddAsync(article, tagList);
    public Task UpdateAsync(NewsArticle article, string tagList) => _dao.UpdateAsync(article, tagList);
    public Task DeleteAsync(string id) => _dao.DeleteAsync(id);
}
