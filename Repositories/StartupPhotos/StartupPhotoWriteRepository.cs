using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupPhotos.Interfaces;

namespace StartupLab.Repositories.StartupPhotos;

/// <summary>
/// EF Core implementation of <see cref="IStartupPhotoWriteRepository"/>.
/// </summary>
public class StartupPhotoWriteRepository(ApplicationDbContext context) : IStartupPhotoWriteRepository
{
    public async Task<StartupPhoto> AddAsync(int startupId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var photo = new StartupPhoto
        {
            StartupId = startupId,
            AddressKey = addressKey,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.StartupPhotos.Add(photo);
        await context.SaveChangesAsync(cancellationToken);
        return photo;
    }

    public async Task<bool> UpdateAsync(int startupId, int photoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.StartupPhotos
            .Where(p => p.Id == photoId && p.StartupId == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Title, title)
                .SetProperty(p => p.Description, description)
                .SetProperty(p => p.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<bool> SetPrimaryAsync(int startupId, int photoId, CancellationToken cancellationToken)
    {
        if (!await context.StartupPhotos.AnyAsync(p => p.Id == photoId && p.StartupId == startupId, cancellationToken))
        {
            return false;
        }

        await context.StartupPhotos
            .Where(p => p.StartupId == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.IsPrimary, p => p.Id == photoId),
                cancellationToken);
        return true;
    }

    public async Task<string?> DeleteAsync(int startupId, int photoId, CancellationToken cancellationToken)
    {
        var photo = await context.StartupPhotos.SingleOrDefaultAsync(p => p.Id == photoId && p.StartupId == startupId, cancellationToken);
        if (photo is null)
        {
            return null;
        }

        context.StartupPhotos.Remove(photo);
        await context.SaveChangesAsync(cancellationToken);
        return photo.AddressKey;
    }
}
