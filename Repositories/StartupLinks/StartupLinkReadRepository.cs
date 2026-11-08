using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupLinks.Interfaces;

namespace StartupLab.Repositories.StartupLinks;

/// <summary>
/// EF Core implementation of <see cref="IStartupLinkReadRepository"/>.
/// </summary>
public class StartupLinkReadRepository(ApplicationDbContext context) : IStartupLinkReadRepository
{
    public async Task<StartupLink?> GetByIdAsync(int startupId, int linkId, CancellationToken cancellationToken) =>
        await context.StartupLinks
            .AsNoTracking()
            .SingleOrDefaultAsync(l => l.Id == linkId && l.StartupId == startupId, cancellationToken);

    public async Task<IReadOnlyList<StartupLink>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken) =>
        await context.StartupLinks
            .AsNoTracking()
            .Where(l => l.StartupId == startupId)
            .OrderBy(l => l.DisplayOrder)
            .ThenBy(l => l.Id)
            .ToListAsync(cancellationToken);
}
