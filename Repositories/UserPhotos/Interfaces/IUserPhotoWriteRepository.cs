namespace StartupLab.Repositories.UserPhotos.Interfaces;

/// <summary>
/// Adds, replaces and deletes user photos. Each user has at most one photo.
/// </summary>
public interface IUserPhotoWriteRepository
{
    /// <summary>
    /// Adds the user's photo, or replaces it if they already have one.
    /// </summary>
    /// <returns>The previous photo's address key, or null if the user had no photo.</returns>
    Task<string?> SetByUserIdAsync(int userId, string addressKey, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the user's photo.
    /// </summary>
    /// <returns>The removed photo's address key, or null if the user had no photo.</returns>
    Task<string?> DeleteByUserIdAsync(int userId, CancellationToken cancellationToken);
}
