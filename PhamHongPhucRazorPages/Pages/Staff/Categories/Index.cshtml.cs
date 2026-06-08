using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace PhamHongPhucRazorPages.Pages.Staff.Categories;

[Authorize(Policy = "StaffOnly")]
public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty]
    public Category Input { get; set; } = new();

    public List<Category> Categories { get; set; } = new();

    [TempData]
    public string? Alert { get; set; }

    public async Task OnGetAsync()
    {
        Categories = await _categoryService.SearchAsync(Search);
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        ModelState.Remove("Input.CategoryId");
        ModelState.Remove("Input.ParentCategory");
        ModelState.Remove("Input.SubCategories");
        ModelState.Remove("Input.NewsArticles");
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        await _categoryService.AddAsync(Input);
        return RedirectToPage(new { Search });
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        ModelState.Remove("Input.ParentCategory");
        ModelState.Remove("Input.SubCategories");
        ModelState.Remove("Input.NewsArticles");
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        await _categoryService.UpdateAsync(Input);
        return RedirectToPage(new { Search });
    }

    public async Task<IActionResult> OnPostDeleteAsync(short id)
    {
        var result = await _categoryService.DeleteAsync(id);
        Alert = result.Message;
        return RedirectToPage(new { Search });
    }
}
