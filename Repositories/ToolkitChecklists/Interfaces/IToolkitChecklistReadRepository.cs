using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitChecklists.Interfaces;

/// <summary>
/// Reads the toolkit checklist and each user's progress on it. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IToolkitChecklistReadRepository
{
    /// <summary>
    /// Gets up to <paramref name="max"/> active toolkit checklist items, in display order.
    /// </summary>
    Task<IReadOnlyList<ToolkitChecklist>> GetItemsAsync(int max, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the user's progress on the toolkit checklist. Items without a row are not done.
    /// </summary>
    Task<IReadOnlyList<ToolkitChecklistProgress>> GetProgressAsync(int userId, CancellationToken cancellationToken);
}
