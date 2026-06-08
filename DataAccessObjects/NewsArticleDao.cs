using BusinessObjects.Models;
using DataAccessObjects.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public class NewsArticleDao
{
    private readonly IDbContextFactory<FunewsDbContext> _contextFactory;

    public NewsArticleDao(IDbContextFactory<FunewsDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<NewsArticle>> SearchAsync(string? keyword, bool activeOnly = false, short? createdById = null)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var query = IncludeDetails(context.NewsArticles).AsNoTracking();

        if (activeOnly)
        {
            query = query.Where(article => article.NewsStatus);
        }

        if (createdById.HasValue)
        {
            query = query.Where(article => article.CreatedById == createdById.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(article => article.NewsTitle.Contains(keyword) ||
                                           article.Headline.Contains(keyword) ||
                                           article.NewsContent.Contains(keyword) ||
                                           article.Tags.Any(tag => tag.TagName.Contains(keyword)));
        }

        return await query.OrderByDescending(article => article.CreatedDate).ToListAsync();
    }

    public async Task<List<NewsArticle>> ReportAsync(DateTime startDate, DateTime endDate)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var end = endDate.Date.AddDays(1).AddTicks(-1);
        return await IncludeDetails(context.NewsArticles).AsNoTracking()
            .Where(article => article.CreatedDate >= startDate.Date && article.CreatedDate <= end)
            .OrderByDescending(article => article.CreatedDate)
            .ToListAsync();
    }

    public async Task<NewsArticle?> GetByIdAsync(string id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await IncludeDetails(context.NewsArticles).AsNoTracking().FirstOrDefaultAsync(article => article.NewsArticleId == id);
    }

    public async Task AddAsync(NewsArticle article, string tagList)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        article.NewsArticleId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        article.CreatedDate = DateTime.Now;
        await AttachTagsAsync(context, article, tagList);
        context.NewsArticles.Add(article);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NewsArticle article, string tagList)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var existing = await context.NewsArticles.Include(item => item.Tags).FirstOrDefaultAsync(item => item.NewsArticleId == article.NewsArticleId);
        if (existing is null)
        {
            return;
        }

        existing.NewsTitle = article.NewsTitle;
        existing.Headline = article.Headline;
        existing.NewsContent = article.NewsContent;
        existing.NewsSource = article.NewsSource;
        existing.CategoryId = article.CategoryId;
        existing.NewsStatus = article.NewsStatus;
        existing.ModifiedDate = DateTime.Now;
        existing.Tags.Clear();
        await AttachTagsAsync(context, existing, tagList);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var article = await context.NewsArticles
            .Include(item => item.Tags)
            .FirstOrDefaultAsync(item => item.NewsArticleId == id);
        if (article is null)
        {
            return;
        }

        article.Tags.Clear();
        context.NewsArticles.Remove(article);
        await context.SaveChangesAsync();
    }

    private static IQueryable<NewsArticle> IncludeDetails(IQueryable<NewsArticle> query)
    {
        return query.Include(article => article.Category)
            .Include(article => article.CreatedBy)
            .Include(article => article.Tags);
    }

    private static async Task AttachTagsAsync(FunewsDbContext context, NewsArticle article, string tagList)
    {
        var names = (tagList ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        var nextTagId = (await context.Tags.MaxAsync(tag => (int?)tag.TagId) ?? 0) + 1;

        foreach (var name in names)
        {
            var tag = await context.Tags.FirstOrDefaultAsync(item => item.TagName == name);
            if (tag is null)
            {
                tag = new Tag { TagId = nextTagId++, TagName = name };
                context.Tags.Add(tag);
            }

            article.Tags.Add(tag);
        }
    }
}
