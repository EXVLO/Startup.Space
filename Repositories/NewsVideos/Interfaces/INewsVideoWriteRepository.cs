using StartupLab.Models.Entities;

namespace StartupLab.Repositories.NewsVideos.Interfaces;

/// <summary>
/// Adds, changes and deletes news videos. A news item can have many, and at most one is primary.
/// </summary>
public interface INewsVideoWriteRepository
{
    /// <summary>
    /// Adds a video to the news item and returns it with its new id. New videos are not primary.
    /// </summary>
    Task<NewsVideo> AddAsync(int newsId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a video's title, description and position. Returns false if it doesn't exist or belongs to another news item.
    /// </summary>
    Task<bool> UpdateAsync(int newsId, int videoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the video the news item's primary video, and every other video of the news item not primary.
    /// Returns false if the video doesn't exist or belongs to another news item.
    /// </summary>
    Task<bool> SetPrimaryAsync(int newsId, int videoId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a video.
    /// </summary>
    /// <returns>The removed video's address key, or null if it doesn't exist or belongs to another news item.</returns>
    Task<string?> DeleteAsync(int newsId, int videoId, CancellationToken cancellationToken);
}
