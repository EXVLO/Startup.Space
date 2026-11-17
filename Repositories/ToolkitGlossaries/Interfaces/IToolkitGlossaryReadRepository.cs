using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitGlossaries.Interfaces;

/// <summary>
/// Reads toolkit glossary entries from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IToolkitGlossaryReadRepository
{
    /// <summary>
    /// Gets a glossary entry by id, or null if not found.
    /// </summary>
    Task<ToolkitGlossary?> GetByIdAsync(int toolkitGlossaryId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets up to <paramref name="max"/> glossary entries, in display order.
    /// </summary>
    Task<IReadOnlyList<ToolkitGlossary>> GetEntriesAsync(int max, CancellationToken cancellationToken);
}
