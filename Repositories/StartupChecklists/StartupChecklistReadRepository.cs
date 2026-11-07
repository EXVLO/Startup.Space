using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupChecklists.Interfaces;

namespace StartupLab.Repositories.StartupChecklists;

/// <summary>
/// EF Core implementation of <see cref="IStartupChecklistReadRepository"/>.
/// </summary>
public class StartupChecklistReadRepository(ApplicationDbContext context) : IStartupChecklistReadRepository
{
    public async Task<IReadOnlyList<StartupChecklist>> GetItemsAsync(int max, CancellationToken cancellationToken) =>
        await context.StartupChecklists
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Id)
            .Take(max)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<StartupChecklistProgress>> GetProgressAsync(int startupId, CancellationToken cancellationToken) =>
        await context.StartupChecklistProgress
            .AsNoTracking()
            .Where(sp => sp.StartupId == startupId)
            .OrderBy(sp => sp.StartupChecklistId)
            .ToListAsync(cancellationToken);
}
