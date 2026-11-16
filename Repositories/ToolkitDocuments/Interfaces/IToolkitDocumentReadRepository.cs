using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitDocuments.Interfaces;

/// <summary>
/// Reads toolkit documents from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IToolkitDocumentReadRepository
{
    /// <summary>
    /// Gets a toolkit document by id, or null if not found.
    /// </summary>
    Task<ToolkitDocument?> GetByIdAsync(int toolkitDocumentId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets up to <paramref name="max"/> toolkit documents, in display order.
    /// </summary>
    Task<IReadOnlyList<ToolkitDocument>> GetDocumentsAsync(int max, CancellationToken cancellationToken);
}
