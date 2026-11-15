using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Categories.Interfaces;

/// <summary>
/// Reads startup categories. Results are not tracked, so changing them saves nothing.
/// Which startups use a category is read from the startup repositories.
/// </summary>
public interface ICategoryReadRepository
{
    /// <summary>
    /// Gets a category by id, or null if not found.
    /// </summary>
    Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets one page of active or inactive categories, sorted by title.
    /// </summary>
    Task<IReadOnlyList<Category>> GetCategoriesAsync(bool isActive, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Counts the categories that <see cref="GetCategoriesAsync"/> would return with the same filter. Used for paging.
    /// </summary>
    Task<int> CountCategoriesAsync(bool isActive, CancellationToken cancellationToken);
}
