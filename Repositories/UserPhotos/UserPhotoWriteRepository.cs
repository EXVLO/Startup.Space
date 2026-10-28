using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.UserPhotos.Interfaces;

namespace StartupLab.Repositories.UserPhotos;

/// <summary>
/// EF Core implementation of <see cref="IUserPhotoWriteRepository"/>.
/// </summary>
public class UserPhotoWriteRepository(ApplicationDbContext context) : IUserPhotoWriteRepository
{
    public async Task<string?> SetByUserIdAsync(int userId, string addressKey, CancellationToken cancellationToken)
    {
        var photo = await context.UserPhotos.SingleOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        var previousAddressKey = photo?.AddressKey;

        if (photo is null)
        {
            photo = new UserPhoto { UserId = userId };
            context.UserPhotos.Add(photo);
        }

        photo.AddressKey = addressKey;
        photo.DateCreated = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return previousAddressKey;
    }

    public async Task<string?> DeleteByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        var photo = await context.UserPhotos.SingleOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        if (photo is null)
        {
            return null;
        }

        context.UserPhotos.Remove(photo);
        await context.SaveChangesAsync(cancellationToken);
        return photo.AddressKey;
    }
}
