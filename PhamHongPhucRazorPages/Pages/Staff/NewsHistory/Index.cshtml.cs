using System.Security.Claims;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Staff.NewsHistory;

[Authorize(Policy = "StaffOnly")]
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
        var staffId = short.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        Articles = await _newsService.SearchAsync(Search, createdById: staffId);
    }
}
