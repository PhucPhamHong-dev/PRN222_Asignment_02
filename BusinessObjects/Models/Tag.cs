using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Tag
{
    [Key]
    public int TagId { get; set; }

    [Required, StringLength(50)]
    public string TagName { get; set; } = string.Empty;

    [StringLength(400)]
    public string? Note { get; set; }

    public ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
