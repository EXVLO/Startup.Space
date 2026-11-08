using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupLinks.Interfaces;

/// <summary>
/// Reads startup links from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IStartupLinkReadRepository
{
    /// <summary>
    /// Gets one link of the startup, or null if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<StartupLink?> GetByIdAsync(int startupId, int linkId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all links of the startup in display order. Empty if it has none.
    /// </summary>
    Task<IReadOnlyList<StartupLink>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken);
}
