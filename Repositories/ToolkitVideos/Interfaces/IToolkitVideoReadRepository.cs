using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitVideos.Interfaces;

/// <summary>
/// Reads toolkit videos from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IToolkitVideoReadRepository
{
    /// <summary>
    /// Gets a toolkit video by id, or null if not found.
    /// </summary>
    Task<ToolkitVideo?> GetByIdAsync(int toolkitVideoId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets up to <paramref name="max"/> toolkit videos, in display order.
    /// </summary>
    Task<IReadOnlyList<ToolkitVideo>> GetVideosAsync(int max, CancellationToken cancellationToken);
}
