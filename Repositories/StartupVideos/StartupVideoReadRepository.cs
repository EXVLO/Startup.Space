using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupVideos.Interfaces;

namespace StartupLab.Repositories.StartupVideos;

/// <summary>
/// EF Core implementation of <see cref="IStartupVideoReadRepository"/>.
/// </summary>
public class StartupVideoReadRepository(ApplicationDbContext context) : IStartupVideoReadRepository
{
    public async Task<StartupVideo?> GetByIdAsync(int startupId, int videoId, CancellationToken cancellationToken) =>
        await context.StartupVideos
            .AsNoTracking()
            .SingleOrDefaultAsync(v => v.Id == videoId && v.StartupId == startupId, cancellationToken);

    public async Task<IReadOnlyList<StartupVideo>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken) =>
        await context.StartupVideos
            .AsNoTracking()
            .Where(v => v.StartupId == startupId)
            .OrderBy(v => v.DisplayOrder)
            .ThenBy(v => v.Id)
            .ToListAsync(cancellationToken);
}
