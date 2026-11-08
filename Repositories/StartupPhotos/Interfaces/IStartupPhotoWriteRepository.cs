using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupPhotos.Interfaces;

/// <summary>
/// Adds, changes and deletes startup photos. A startup can have many, and at most one is primary.
/// </summary>
public interface IStartupPhotoWriteRepository
{
    /// <summary>
    /// Adds a photo to the startup and returns it with its new id. New photos are not primary.
    /// </summary>
    Task<StartupPhoto> AddAsync(int startupId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a photo's title, description and position. Returns false if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<bool> UpdateAsync(int startupId, int photoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the photo the startup's primary photo, and every other photo of the startup not primary.
    /// Returns false if the photo doesn't exist or belongs to another startup.
    /// </summary>
    Task<bool> SetPrimaryAsync(int startupId, int photoId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a photo.
    /// </summary>
    /// <returns>The removed photo's address key, or null if it doesn't exist or belongs to another startup.</returns>
    Task<string?> DeleteAsync(int startupId, int photoId, CancellationToken cancellationToken);
}
