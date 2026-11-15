using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.NewsPhotos.Interfaces;

namespace StartupLab.Repositories.NewsPhotos;

/// <summary>
/// EF Core implementation of <see cref="INewsPhotoWriteRepository"/>.
/// </summary>
public class NewsPhotoWriteRepository(ApplicationDbContext context) : INewsPhotoWriteRepository
{
    public async Task<NewsPhoto> AddAsync(int newsId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var photo = new NewsPhoto
        {
            NewsId = newsId,
            AddressKey = addressKey,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.NewsPhotos.Add(photo);
        await context.SaveChangesAsync(cancellationToken);
        return photo;
    }

    public async Task<bool> UpdateAsync(int newsId, int photoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.NewsPhotos
            .Where(p => p.Id == photoId && p.NewsId == newsId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Title, title)
                .SetProperty(p => p.Description, description)
                .SetProperty(p => p.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<bool> SetPrimaryAsync(int newsId, int photoId, CancellationToken cancellationToken)
    {
        if (!await context.NewsPhotos.AnyAsync(p => p.Id == photoId && p.NewsId == newsId, cancellationToken))
        {
            return false;
        }

        await context.NewsPhotos
            .Where(p => p.NewsId == newsId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.IsPrimary, p => p.Id == photoId),
                cancellationToken);
        return true;
    }

    public async Task<string?> DeleteAsync(int newsId, int photoId, CancellationToken cancellationToken)
    {
        var photo = await context.NewsPhotos.SingleOrDefaultAsync(p => p.Id == photoId && p.NewsId == newsId, cancellationToken);
        if (photo is null)
        {
            return null;
        }

        context.NewsPhotos.Remove(photo);
        await context.SaveChangesAsync(cancellationToken);
        return photo.AddressKey;
    }
}
