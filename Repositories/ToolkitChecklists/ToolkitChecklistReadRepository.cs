using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitChecklists.Interfaces;

namespace StartupLab.Repositories.ToolkitChecklists;

/// <summary>
/// EF Core implementation of <see cref="IToolkitChecklistReadRepository"/>.
/// </summary>
public class ToolkitChecklistReadRepository(ApplicationDbContext context) : IToolkitChecklistReadRepository
{
    public async Task<IReadOnlyList<ToolkitChecklist>> GetItemsAsync(int max, CancellationToken cancellationToken) =>
        await context.ToolkitChecklists
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Id)
            .Take(max)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<ToolkitChecklistProgress>> GetProgressAsync(int userId, CancellationToken cancellationToken) =>
        await context.ToolkitChecklistProgress
            .AsNoTracking()
            .Where(tp => tp.UserId == userId)
            .OrderBy(tp => tp.ToolkitChecklistId)
            .ToListAsync(cancellationToken);
}
