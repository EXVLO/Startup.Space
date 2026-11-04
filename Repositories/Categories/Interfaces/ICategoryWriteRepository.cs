using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Categories.Interfaces;

/// <summary>
/// Adds, changes and activates or deactivates categories.
/// </summary>
public interface ICategoryWriteRepository
{
    /// <summary>
    /// Adds a category and returns it with its new id.
    /// </summary>
    Task<Category> AddAsync(string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a category. Returns false if it doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int categoryId, string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Shows or hides a category. Returns false if it doesn't exist.
    /// </summary>
    Task<bool> SetActiveAsync(int categoryId, bool isActive, CancellationToken cancellationToken);
}
