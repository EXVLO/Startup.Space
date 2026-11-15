using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Tags.Interfaces;

/// <summary>
/// Reads news tags. Results are not tracked, so changing them saves nothing.
/// Which news items use a tag is read from the news repositories.
/// </summary>
public interface ITagReadRepository
{
    /// <summary>
    /// Gets a tag by id, or null if not found.
    /// </summary>
    Task<Tag?> GetByIdAsync(int tagId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets one page of active or inactive tags, sorted by title.
    /// </summary>
    Task<IReadOnlyList<Tag>> GetTagsAsync(bool isActive, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Counts the tags that <see cref="GetTagsAsync"/> would return with the same filter. Used for paging.
    /// </summary>
    Task<int> CountTagsAsync(bool isActive, CancellationToken cancellationToken);
}
