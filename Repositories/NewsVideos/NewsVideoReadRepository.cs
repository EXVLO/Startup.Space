using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.NewsVideos.Interfaces;

namespace StartupLab.Repositories.NewsVideos;

/// <summary>
/// EF Core implementation of <see cref="INewsVideoReadRepository"/>.
/// </summary>
public class NewsVideoReadRepository(ApplicationDbContext context) : INewsVideoReadRepository
{
    public async Task<NewsVideo?> GetByIdAsync(int newsId, int videoId, CancellationToken cancellationToken) =>
        await context.NewsVideos
            .AsNoTracking()
            .SingleOrDefaultAsync(v => v.Id == videoId && v.NewsId == newsId, cancellationToken);

    public async Task<IReadOnlyList<NewsVideo>> GetByNewsIdAsync(int newsId, CancellationToken cancellationToken) =>
        await context.NewsVideos
            .AsNoTracking()
            .Where(v => v.NewsId == newsId)
            .OrderBy(v => v.DisplayOrder)
            .ThenBy(v => v.Id)
            .ToListAsync(cancellationToken);
}
