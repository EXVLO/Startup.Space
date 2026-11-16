using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitVideos.Interfaces;

/// <summary>
/// Adds, changes and deletes toolkit videos. They have no <c>is_active</c>, so they are only hard deleted.
/// </summary>
public interface IToolkitVideoWriteRepository
{
    /// <summary>
    /// Adds a toolkit video and returns it with its new id.
    /// </summary>
    Task<ToolkitVideo> CreateAsync(string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a video's title, description and position. Returns false if the video doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int toolkitVideoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a toolkit video.
    /// </summary>
    /// <returns>The removed video's address key, or null if the video doesn't exist.</returns>
    Task<string?> DeleteAsync(int toolkitVideoId, CancellationToken cancellationToken);
}
