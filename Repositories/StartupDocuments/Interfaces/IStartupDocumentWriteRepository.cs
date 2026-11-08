using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupDocuments.Interfaces;

/// <summary>
/// Adds, changes and deletes startup documents. A startup can have many.
/// </summary>
public interface IStartupDocumentWriteRepository
{
    /// <summary>
    /// Adds a document to the startup and returns it with its new id.
    /// </summary>
    Task<StartupDocument> AddAsync(int startupId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a document's title, description and position. Returns false if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<bool> UpdateAsync(int startupId, int documentId, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a document.
    /// </summary>
    /// <returns>The removed document's address key, or null if it doesn't exist or belongs to another startup.</returns>
    Task<string?> DeleteAsync(int startupId, int documentId, CancellationToken cancellationToken);
}
