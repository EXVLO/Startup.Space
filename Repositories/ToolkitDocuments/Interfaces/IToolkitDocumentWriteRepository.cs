using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitDocuments.Interfaces;

/// <summary>
/// Adds, changes and deletes toolkit documents. They are links, not stored files,
/// and have no <c>is_active</c>, so they are only hard deleted.
/// </summary>
public interface IToolkitDocumentWriteRepository
{
    /// <summary>
    /// Adds a toolkit document and returns it with its new id.
    /// </summary>
    Task<ToolkitDocument> CreateAsync(string url, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a document's URL, title, description and position. Returns false if the document doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int toolkitDocumentId, string url, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a toolkit document. Returns false if the document doesn't exist.
    /// </summary>
    Task<bool> DeleteAsync(int toolkitDocumentId, CancellationToken cancellationToken);
}
