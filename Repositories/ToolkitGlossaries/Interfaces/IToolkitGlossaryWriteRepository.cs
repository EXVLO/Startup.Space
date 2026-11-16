using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitGlossaries.Interfaces;

/// <summary>
/// Adds, changes and deletes toolkit glossary entries. They have no <c>is_active</c>, so they are only hard deleted.
/// </summary>
public interface IToolkitGlossaryWriteRepository
{
    /// <summary>
    /// Adds a glossary entry and returns it with its new id.
    /// </summary>
    Task<ToolkitGlossary> CreateAsync(string term, string definition, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes an entry's term, definition and position. Returns false if the entry doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int toolkitGlossaryId, string term, string definition, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a glossary entry. Returns false if the entry doesn't exist.
    /// </summary>
    Task<bool> DeleteAsync(int toolkitGlossaryId, CancellationToken cancellationToken);
}
