using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Categories.Interfaces;

/// <summary>
/// Adds and changes startup categories. A category is only soft deleted, because it has <c>is_active</c>.
/// </summary>
public interface ICategoryWriteRepository
{
    /// <summary>
    /// Adds a category and returns it with its new id.
    /// </summary>
    Task<Category> CreateAsync(string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the category's title and description. Returns false if the category doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int categoryId, string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Makes an inactive category active again. Returns false if the category doesn't exist.
    /// </summary>
    Task<bool> ActivateAsync(int categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the category inactive (soft delete). It stays in the database and is hidden from filters.
    /// Returns false if the category doesn't exist.
    /// </summary>
    Task<bool> DeactivateAsync(int categoryId, CancellationToken cancellationToken);
}
