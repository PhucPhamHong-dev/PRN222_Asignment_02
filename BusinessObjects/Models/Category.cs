using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Category
{
    [Key]
    public short CategoryId { get; set; }

    [Required, StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(250)]
    public string CategoryDescription { get; set; } = string.Empty;

    public short? ParentCategoryId { get; set; }

    public bool CategoryStatus { get; set; } = true;

    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
