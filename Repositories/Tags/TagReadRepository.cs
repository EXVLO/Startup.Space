using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Tags.Interfaces;

namespace StartupLab.Repositories.Tags;

/// <summary>
/// EF Core implementation of <see cref="ITagReadRepository"/>.
/// </summary>
public class TagReadRepository(ApplicationDbContext context) : ITagReadRepository
{
    public async Task<Tag?> GetByIdAsync(int tagId, CancellationToken cancellationToken) =>
        await context.Tags
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == tagId, cancellationToken);

    public async Task<IReadOnlyList<Tag>> GetTagsAsync(bool isActive, int skip, int take, CancellationToken cancellationToken) =>
        await context.Tags
            .AsNoTracking()
            .Where(t => t.IsActive == isActive)
            .OrderBy(t => t.Title)
            .ThenBy(t => t.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    public async Task<int> CountTagsAsync(bool isActive, CancellationToken cancellationToken) =>
        await context.Tags.CountAsync(t => t.IsActive == isActive, cancellationToken);
}
