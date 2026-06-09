using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Admin.Accounts;

[Authorize(Policy = "AdminOnly")]
public class IndexModel : PageModel
{
    private readonly IAccountService _accountService;

    public IndexModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty]
    public SystemAccount Input { get; set; } = new();

    public List<SystemAccount> Accounts { get; set; } = new();
    public string? Message { get; set; }

    public async Task OnGetAsync()
    {
        Accounts = await _accountService.SearchAsync(Search);
    }

    public async Task<PartialViewResult> OnGetSearchAsync(string? keyword)
    {
        var accounts = await _accountService.SearchAsync(keyword);
        return Partial("_AccountTablePartial", accounts);
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        ModelState.Remove("Input.AccountId");
        ModelState.Remove("Input.NewsArticles");

        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        await _accountService.AddAsync(Input);
        return RedirectToPage(new { Search });
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        ModelState.Remove("Input.NewsArticles");

        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        await _accountService.UpdateAsync(Input);
        return RedirectToPage(new { Search });
    }

    public async Task<IActionResult> OnPostDeleteAsync(short id)
    {
        await _accountService.DeleteAsync(id);
        return RedirectToPage(new { Search });
    }
}
