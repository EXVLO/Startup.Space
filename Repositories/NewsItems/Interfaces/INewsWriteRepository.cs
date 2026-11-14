using StartupLab.Models.Entities;

namespace StartupLab.Repositories.NewsItems.Interfaces;

/// <summary>
/// Adds, changes and deletes news items, and changes which one is featured and which tags it has.
/// News has no <c>is_active</c>, so it is only hard deleted.
/// </summary>
public interface INewsWriteRepository
{
    /// <summary>
    /// Adds a news item and returns it with its new id. New items are not featured.
    /// </summary>
    Task<News> CreateAsync(string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the news item's title and description. Returns false if it doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int newsId, string title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the news item the featured one, and every other news item not featured.
    /// Returns false if it doesn't exist.
    /// </summary>
    Task<bool> SetPrimaryAsync(int newsId, CancellationToken cancellationToken);

    /// <summary>
    /// Replaces the news item's tags with <paramref name="tagIds"/>. Ids of tags that don't exist are skipped.
    /// Returns false if the news item doesn't exist.
    /// </summary>
    Task<bool> SetTagsAsync(int newsId, IReadOnlyCollection<int> tagIds, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the news item for good, with its photos, videos and tag links.
    /// </summary>
    /// <returns>The address keys of the removed photos and videos, or null if the news item doesn't exist.</returns>
    Task<IReadOnlyList<string>?> DeleteAsync(int newsId, CancellationToken cancellationToken);
}
