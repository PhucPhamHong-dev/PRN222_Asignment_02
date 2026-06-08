using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Lecturer.News;

[Authorize(Policy = "LecturerOnly")]
public class IndexModel : PageModel
{
    private readonly INewsArticleService _newsService;

    public IndexModel(INewsArticleService newsService)
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
