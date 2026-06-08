using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Admin.Reports;

[Authorize(Policy = "AdminOnly")]
public class IndexModel : PageModel
{
    private readonly INewsArticleService _newsService;

    public IndexModel(INewsArticleService newsService)
    {
        _newsService = newsService;
    }

    [BindProperty(SupportsGet = true)]
    public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-30);

    [BindProperty(SupportsGet = true)]
    public DateTime EndDate { get; set; } = DateTime.Today;

    public List<NewsArticle> Articles { get; set; } = new();

    public async Task OnGetAsync()
    {
        Articles = await _newsService.ReportAsync(StartDate, EndDate);
    }
}
