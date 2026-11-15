using StartupLab.Models.Entities;

namespace StartupLab.Repositories.NewsPhotos.Interfaces;

/// <summary>
/// Adds, changes and deletes news photos. A news item can have many, and at most one is primary.
/// </summary>
public interface INewsPhotoWriteRepository
{
    /// <summary>
    /// Adds a photo to the news item and returns it with its new id. New photos are not primary.
    /// </summary>
    Task<NewsPhoto> AddAsync(int newsId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a photo's title, description and position. Returns false if it doesn't exist or belongs to another news item.
    /// </summary>
    Task<bool> UpdateAsync(int newsId, int photoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the photo the news item's primary photo, and every other photo of the news item not primary.
    /// Returns false if the photo doesn't exist or belongs to another news item.
    /// </summary>
    Task<bool> SetPrimaryAsync(int newsId, int photoId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a photo.
    /// </summary>
    /// <returns>The removed photo's address key, or null if it doesn't exist or belongs to another news item.</returns>
    Task<string?> DeleteAsync(int newsId, int photoId, CancellationToken cancellationToken);
}
