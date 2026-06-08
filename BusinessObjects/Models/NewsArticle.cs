using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class NewsArticle
{
    [Key]
    [StringLength(20)]
    public string NewsArticleId { get; set; } = string.Empty;

    [Required, StringLength(400)]
    public string NewsTitle { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Headline { get; set; } = string.Empty;

    [Required, StringLength(4000)]
    public string NewsContent { get; set; } = string.Empty;

    [StringLength(400)]
    public string? NewsSource { get; set; }

    [Required]
    public short? CategoryId { get; set; }

    public bool NewsStatus { get; set; } = true;

    public short? CreatedById { get; set; }

    public short? UpdatedById { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

    public Category? Category { get; set; }
    public SystemAccount? CreatedBy { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
