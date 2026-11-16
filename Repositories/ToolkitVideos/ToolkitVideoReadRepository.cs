using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitVideos.Interfaces;

namespace StartupLab.Repositories.ToolkitVideos;

/// <summary>
/// EF Core implementation of <see cref="IToolkitVideoReadRepository"/>.
/// </summary>
public class ToolkitVideoReadRepository(ApplicationDbContext context) : IToolkitVideoReadRepository
{
    public async Task<ToolkitVideo?> GetByIdAsync(int toolkitVideoId, CancellationToken cancellationToken) =>
        await context.ToolkitVideos
            .AsNoTracking()
            .SingleOrDefaultAsync(v => v.Id == toolkitVideoId, cancellationToken);

    public async Task<IReadOnlyList<ToolkitVideo>> GetVideosAsync(int max, CancellationToken cancellationToken) =>
        await context.ToolkitVideos
            .AsNoTracking()
            .OrderBy(v => v.DisplayOrder)
            .ThenBy(v => v.Id)
            .Take(max)
            .ToListAsync(cancellationToken);
}
