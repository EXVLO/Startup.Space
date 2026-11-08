using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupVideos.Interfaces;

/// <summary>
/// Reads startup videos from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IStartupVideoReadRepository
{
    /// <summary>
    /// Gets one video of the startup, or null if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<StartupVideo?> GetByIdAsync(int startupId, int videoId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all videos of the startup in display order. Empty if it has none.
    /// </summary>
    Task<IReadOnlyList<StartupVideo>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken);
}
