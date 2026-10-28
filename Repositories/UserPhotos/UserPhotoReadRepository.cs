using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.UserPhotos.Interfaces;

namespace StartupLab.Repositories.UserPhotos;

/// <summary>
/// EF Core implementation of <see cref="IUserPhotoReadRepository"/>.
/// </summary>
public class UserPhotoReadRepository(ApplicationDbContext context) : IUserPhotoReadRepository
{
    public Task<UserPhoto?> GetByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        context.UserPhotos
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.UserId == userId, cancellationToken);
}
