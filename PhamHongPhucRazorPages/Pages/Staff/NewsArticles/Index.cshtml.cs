using System.Security.Claims;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PhamHongPhucRazorPages.Hubs;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Staff.NewsArticles;

[Authorize(Policy = "StaffOnly")]
public class IndexModel : PageModel
{
    private readonly INewsArticleService _newsService;
    private readonly ICategoryService _categoryService;
    private readonly IHubContext<NewsHub> _hubContext;

    public IndexModel(INewsArticleService newsService, ICategoryService categoryService, IHubContext<NewsHub> hubContext)
    {
        _newsService = newsService;
        _categoryService = categoryService;
        _hubContext = hubContext;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty]
    public NewsArticle Input { get; set; } = new();

    [BindProperty]
    public string TagList { get; set; } = string.Empty;

    public List<NewsArticle> Articles { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    public async Task OnGetAsync()
    {
        Articles = await _newsService.SearchAsync(Search);
        Categories = await _categoryService.GetActiveAsync();
    }

    public async Task<PartialViewResult> OnGetSearchAsync(string? keyword)
    {
        var articles = await _newsService.SearchAsync(keyword);
        return Partial("_NewsArticleTablePartial", articles);
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        CleanNewsModelState();
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        Input.CreatedById = CurrentAccountId();
        await _newsService.AddAsync(Input, TagList);
        await _hubContext.Clients.All.SendAsync("ReceiveNewsUpdate");
        return RedirectToPage(new { Search });
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        CleanNewsModelState();
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        await _newsService.UpdateAsync(Input, TagList);
        await _hubContext.Clients.All.SendAsync("ReceiveNewsUpdate");
        return RedirectToPage(new { Search });
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        await _newsService.DeleteAsync(id);
        await _hubContext.Clients.All.SendAsync("ReceiveNewsUpdate");
        return RedirectToPage(new { Search });
    }

    private short CurrentAccountId()
    {
        return short.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
    }

    private void CleanNewsModelState()
    {
        ModelState.Remove("Input.NewsArticleId");
        ModelState.Remove("Input.Category");
        ModelState.Remove("Input.CreatedBy");
        ModelState.Remove("Input.Tags");
    }
}
