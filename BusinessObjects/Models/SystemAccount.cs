using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class SystemAccount
{
    [Key]
    public short AccountId { get; set; }

    [Required, StringLength(100)]
    public string AccountName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(70)]
    public string AccountEmail { get; set; } = string.Empty;

    [Required, StringLength(70)]
    public string AccountPassword { get; set; } = string.Empty;

    [Range(1, 2)]
    public int AccountRole { get; set; }

    public ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
