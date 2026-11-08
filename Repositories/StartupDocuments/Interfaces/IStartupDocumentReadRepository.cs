using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupDocuments.Interfaces;

/// <summary>
/// Reads startup documents from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IStartupDocumentReadRepository
{
    /// <summary>
    /// Gets one document of the startup, or null if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<StartupDocument?> GetByIdAsync(int startupId, int documentId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all documents of the startup in display order. Empty if it has none.
    /// </summary>
    Task<IReadOnlyList<StartupDocument>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken);
}
