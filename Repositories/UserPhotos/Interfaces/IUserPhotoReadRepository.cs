using StartupLab.Models.Entities;

namespace StartupLab.Repositories.UserPhotos.Interfaces;

/// <summary>
/// Reads user photos from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IUserPhotoReadRepository
{
    /// <summary>
    /// Gets the user's photo, or null if they have none.
    /// </summary>
    Task<UserPhoto?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
}
