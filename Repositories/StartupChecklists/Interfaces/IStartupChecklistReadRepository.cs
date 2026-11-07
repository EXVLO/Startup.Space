using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupChecklists.Interfaces;

/// <summary>
/// Reads the startup checklist and each startup's progress on it. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IStartupChecklistReadRepository
{
    /// <summary>
    /// Gets up to <paramref name="max"/> active startup checklist items, in display order.
    /// </summary>
    Task<IReadOnlyList<StartupChecklist>> GetItemsAsync(int max, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the startup's progress on the startup checklist. Items without a row are not done.
    /// </summary>
    Task<IReadOnlyList<StartupChecklistProgress>> GetProgressAsync(int startupId, CancellationToken cancellationToken);
}
