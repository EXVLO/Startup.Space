using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.NewsPhotos.Interfaces;

namespace StartupLab.Repositories.NewsPhotos;

/// <summary>
/// EF Core implementation of <see cref="INewsPhotoReadRepository"/>.
/// </summary>
public class NewsPhotoReadRepository(ApplicationDbContext context) : INewsPhotoReadRepository
{
    public async Task<NewsPhoto?> GetByIdAsync(int newsId, int photoId, CancellationToken cancellationToken) =>
        await context.NewsPhotos
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == photoId && p.NewsId == newsId, cancellationToken);

    public async Task<IReadOnlyList<NewsPhoto>> GetByNewsIdAsync(int newsId, CancellationToken cancellationToken) =>
        await context.NewsPhotos
            .AsNoTracking()
            .Where(p => p.NewsId == newsId)
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Id)
            .ToListAsync(cancellationToken);
}
