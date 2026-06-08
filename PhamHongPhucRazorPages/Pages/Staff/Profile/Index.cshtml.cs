using System.Security.Claims;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Staff.Profile;

[Authorize(Policy = "StaffOnly")]
public class IndexModel : PageModel
{
    private readonly IAccountService _accountService;

    public IndexModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [BindProperty]
    public SystemAccount Input { get; set; } = new();

    [TempData]
    public string? Message { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var account = await _accountService.GetByIdAsync(CurrentAccountId());
        if (account is null)
        {
            return RedirectToPage("/Logout");
        }

        Input = account;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Input.AccountId = CurrentAccountId();
        Input.AccountRole = AppRoles.Staff;
        ModelState.Remove("Input.NewsArticles");
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _accountService.UpdateAsync(Input);
        Message = "Profile updated successfully.";
        return RedirectToPage();
    }

    private short CurrentAccountId() => short.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
}
