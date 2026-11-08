using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupVideos.Interfaces;

/// <summary>
/// Adds, changes and deletes startup videos. A startup can have many.
/// </summary>
public interface IStartupVideoWriteRepository
{
    /// <summary>
    /// Adds a video to the startup and returns it with its new id.
    /// </summary>
    Task<StartupVideo> AddAsync(int startupId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a video's title, description and position. Returns false if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<bool> UpdateAsync(int startupId, int videoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a video.
    /// </summary>
    /// <returns>The removed video's address key, or null if it doesn't exist or belongs to another startup.</returns>
    Task<string?> DeleteAsync(int startupId, int videoId, CancellationToken cancellationToken);
}
