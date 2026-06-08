using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.News;

public class PublicIndexModel : PageModel
{
    private readonly INewsArticleService _newsService;

    public PublicIndexModel(INewsArticleService newsService)
    {
        _newsService = newsService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<NewsArticle> Articles { get; set; } = new();

    public async Task OnGetAsync()
    {
        Articles = await _newsService.SearchAsync(Search, activeOnly: true);
    }
}
