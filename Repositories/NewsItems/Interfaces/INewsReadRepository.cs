using StartupLab.Models.Entities;

namespace StartupLab.Repositories.NewsItems.Interfaces;

/// <summary>
/// Reads news items and their tags. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface INewsReadRepository
{
    /// <summary>
    /// Gets a news item by id, or null if not found.
    /// </summary>
    Task<News?> GetByIdAsync(int newsId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a news item with its photos, videos and tags, or null if not found.
    /// </summary>
    Task<News?> GetDetailsAsync(int newsId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the featured news item (the one with <c>IsPrimary</c>) with its primary photo and tags, or null if none is featured.
    /// </summary>
    Task<News?> GetPrimaryAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets one page of news items with their primary photo and tags, newest first.
    /// A null <paramref name="tagId"/> is not applied.
    /// </summary>
    Task<IReadOnlyList<News>> GetNewsAsync(int? tagId, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Counts the news items that <see cref="GetNewsAsync"/> would return with the same tag filter. Used for paging.
    /// </summary>
    Task<int> CountNewsAsync(int? tagId, CancellationToken cancellationToken);
}
