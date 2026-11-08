using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Categories.Interfaces;

/// <summary>
/// Reads categories from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface ICategoryReadRepository
{
    /// <summary>
    /// Gets one category, or null if it doesn't exist.
    /// </summary>
    Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the active categories, by title. Empty if there are none.
    /// </summary>
    Task<IReadOnlyList<Category>> GetActiveAsync(int max, CancellationToken cancellationToken);
}
