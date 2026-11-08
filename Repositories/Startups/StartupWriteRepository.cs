using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Startups.Interfaces;

namespace StartupLab.Repositories.Startups;

/// <summary>
/// EF Core implementation of <see cref="IStartupWriteRepository"/>.
/// </summary>
public class StartupWriteRepository(ApplicationDbContext context) : IStartupWriteRepository
{
    public async Task<Startup> CreateAsync(int userId, string name, string? description, CancellationToken cancellationToken)
    {
        var startup = new Startup
        {
            Name = name,
            Description = description,
            DateCreated = DateTime.UtcNow
        };
        startup.UserStartups.Add(new UserStartup { UserId = userId });

        context.Startups.Add(startup);
        await context.SaveChangesAsync(cancellationToken);
        return startup;
    }

    public async Task<bool> UpdateAsync(int startupId, string name, string? description, CancellationToken cancellationToken) =>
        await context.Startups
            .Where(st => st.Id == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(st => st.Name, name)
                .SetProperty(st => st.Description, description)
                .SetProperty(st => st.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> ActivateAsync(int startupId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(startupId, true, cancellationToken);

    public async Task<bool> DeactivateAsync(int startupId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(startupId, false, cancellationToken);

    public async Task<IReadOnlyList<string>?> DeleteAsync(int startupId, CancellationToken cancellationToken)
    {
        var addressKeys = await context.StartupPhotos
            .Where(p => p.StartupId == startupId)
            .Select(p => p.AddressKey)
            .Concat(context.StartupVideos
                .Where(v => v.StartupId == startupId)
                .Select(v => v.AddressKey))
            .Concat(context.StartupDocuments
                .Where(d => d.StartupId == startupId)
                .Select(d => d.AddressKey))
            .ToListAsync(cancellationToken);

        var deleted = await context.Startups
            .Where(st => st.Id == startupId)
            .ExecuteDeleteAsync(cancellationToken) > 0;

        return deleted ? addressKeys : null;
    }

    public async Task<bool> AddFounderAsync(int startupId, int userId, CancellationToken cancellationToken)
    {
        if (!await context.Startups.AnyAsync(st => st.Id == startupId, cancellationToken)
            || !await context.Users.AnyAsync(u => u.Id == userId && u.IsActive, cancellationToken))
        {
            return false;
        }

        if (await context.UserStartups.AnyAsync(us => us.StartupId == startupId && us.UserId == userId, cancellationToken))
        {
            return true;
        }

        context.UserStartups.Add(new UserStartup { StartupId = startupId, UserId = userId });
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RemoveFounderAsync(int startupId, int userId, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        await StartupFounderRemoval.LockAsync(context, [startupId], cancellationToken);

        var removed = await context.UserStartups
            .Where(us => us.StartupId == startupId && us.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
        if (!removed)
        {
            return false;
        }

        await StartupFounderRemoval.DeactivateWithoutFoundersAsync(context, [startupId], cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return true;
    }

    public async Task<bool> SetCategoriesAsync(int startupId, IReadOnlyCollection<int> categoryIds, CancellationToken cancellationToken)
    {
        if (!await context.Startups.AnyAsync(st => st.Id == startupId, cancellationToken))
        {
            return false;
        }

        var newCategoryIds = await context.Categories
            .Where(c => categoryIds.Contains(c.Id))
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);

        var current = await context.StartupCategories
            .Where(sc => sc.StartupId == startupId)
            .ToListAsync(cancellationToken);

        context.StartupCategories.RemoveRange(current.Where(sc => !newCategoryIds.Contains(sc.CategoryId)));
        context.StartupCategories.AddRange(newCategoryIds
            .Except(current.Select(sc => sc.CategoryId))
            .Select(categoryId => new StartupCategory { StartupId = startupId, CategoryId = categoryId }));

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<bool> UpdateIsActiveAsync(int startupId, bool isActive, CancellationToken cancellationToken) =>
        await context.Startups
            .Where(st => st.Id == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(st => st.IsActive, isActive)
                .SetProperty(st => st.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;
}
