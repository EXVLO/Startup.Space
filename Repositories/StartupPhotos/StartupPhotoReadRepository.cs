using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupPhotos.Interfaces;

namespace StartupLab.Repositories.StartupPhotos;

/// <summary>
/// EF Core implementation of <see cref="IStartupPhotoReadRepository"/>.
/// </summary>
public class StartupPhotoReadRepository(ApplicationDbContext context) : IStartupPhotoReadRepository
{
    public async Task<StartupPhoto?> GetByIdAsync(int startupId, int photoId, CancellationToken cancellationToken) =>
        await context.StartupPhotos
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == photoId && p.StartupId == startupId, cancellationToken);

    public async Task<IReadOnlyList<StartupPhoto>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken) =>
        await context.StartupPhotos
            .AsNoTracking()
            .Where(p => p.StartupId == startupId)
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Id)
            .ToListAsync(cancellationToken);
}
