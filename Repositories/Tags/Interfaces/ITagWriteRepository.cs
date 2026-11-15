using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Tags.Interfaces;

/// <summary>
/// Adds and changes news tags. A tag is only soft deleted, because it has <c>is_active</c>.
/// </summary>
public interface ITagWriteRepository
{
    /// <summary>
    /// Adds a tag and returns it with its new id.
    /// </summary>
    Task<Tag> CreateAsync(string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the tag's title and description. Returns false if the tag doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int tagId, string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Makes an inactive tag active again. Returns false if the tag doesn't exist.
    /// </summary>
    Task<bool> ActivateAsync(int tagId, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the tag inactive (soft delete). It stays in the database and is hidden from filters.
    /// Returns false if the tag doesn't exist.
    /// </summary>
    Task<bool> DeactivateAsync(int tagId, CancellationToken cancellationToken);
}
