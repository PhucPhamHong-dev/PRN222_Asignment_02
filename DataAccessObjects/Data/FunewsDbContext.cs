using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects.Data;

public class FunewsDbContext : DbContext
{
    public FunewsDbContext(DbContextOptions<FunewsDbContext> options) : base(options)
    {
    }

    public DbSet<SystemAccount> SystemAccounts => Set<SystemAccount>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.ToTable("SystemAccount");
            entity.Property(account => account.AccountId).HasColumnName("AccountID").ValueGeneratedNever();
        });

        modelBuilder.Entity<SystemAccount>()
            .HasIndex(account => account.AccountEmail)
            .IsUnique();

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
            entity.Property(category => category.CategoryId).HasColumnName("CategoryID").ValueGeneratedOnAdd();
            entity.Property(category => category.CategoryDescription).HasColumnName("CategoryDesciption");
            entity.Property(category => category.ParentCategoryId).HasColumnName("ParentCategoryID");
            entity.Property(category => category.CategoryStatus).HasColumnName("IsActive");
            entity.HasOne(category => category.ParentCategory)
                .WithMany(category => category.SubCategories)
                .HasForeignKey(category => category.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<NewsArticle>(entity =>
        {
            entity.ToTable("NewsArticle");
            entity.Property(article => article.NewsArticleId).HasColumnName("NewsArticleID");
            entity.Property(article => article.CategoryId).HasColumnName("CategoryID");
            entity.Property(article => article.CreatedById).HasColumnName("CreatedByID");
            entity.Property(article => article.UpdatedById).HasColumnName("UpdatedByID");
            entity.HasOne(article => article.Category)
                .WithMany(category => category.NewsArticles)
                .HasForeignKey(article => article.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(article => article.CreatedBy)
                .WithMany(account => account.NewsArticles)
                .HasForeignKey(article => article.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");
            entity.Property(tag => tag.TagId).HasColumnName("TagID").ValueGeneratedNever();
        });

        modelBuilder.Entity<NewsArticle>()
            .HasMany(article => article.Tags)
            .WithMany(tag => tag.NewsArticles)
            .UsingEntity<Dictionary<string, object>>(
                "NewsTag",
                join => join.HasOne<Tag>().WithMany().HasForeignKey("TagID").OnDelete(DeleteBehavior.Cascade),
                join => join.HasOne<NewsArticle>().WithMany().HasForeignKey("NewsArticleID").OnDelete(DeleteBehavior.Cascade),
                join => join.ToTable("NewsTag"));
    }
}
