using BusinessObjects.Models;
using DataAccessObjects.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public class AccountDao
{
    private readonly IDbContextFactory<FunewsDbContext> _contextFactory;

    public AccountDao(IDbContextFactory<FunewsDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<SystemAccount>> SearchAsync(string? keyword)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var query = context.SystemAccounts.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim();
            var roleKeyword = keyword.Equals("staff", StringComparison.OrdinalIgnoreCase)
                ? AppRoles.Staff
                : keyword.Equals("lecturer", StringComparison.OrdinalIgnoreCase)
                    ? AppRoles.Lecturer
                    : (int?)null;

            query = query.Where(account =>
                account.AccountName.Contains(keyword) ||
                account.AccountEmail.Contains(keyword) ||
                (roleKeyword.HasValue && account.AccountRole == roleKeyword.Value));
        }

        return await query.OrderBy(account => account.AccountName).ToListAsync();
    }

    public async Task<SystemAccount?> GetByIdAsync(short id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.SystemAccounts.AsNoTracking().FirstOrDefaultAsync(account => account.AccountId == id);
    }

    public async Task<SystemAccount?> GetByEmailAsync(string email)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.SystemAccounts.AsNoTracking().FirstOrDefaultAsync(account => account.AccountEmail == email);
    }

    public async Task AddAsync(SystemAccount account)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        account.AccountId = await NextIdAsync(context);
        context.SystemAccounts.Add(account);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SystemAccount account)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.SystemAccounts.Update(account);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(short id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var account = await context.SystemAccounts.FindAsync(id);
        if (account is null)
        {
            return;
        }

        context.SystemAccounts.Remove(account);
        await context.SaveChangesAsync();
    }

    private static async Task<short> NextIdAsync(FunewsDbContext context)
    {
        var maxId = await context.SystemAccounts.MaxAsync(account => (short?)account.AccountId) ?? 0;
        return (short)(maxId + 1);
    }
}
