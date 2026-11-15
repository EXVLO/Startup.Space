using StartupLab.Models.Entities;

namespace StartupLab.Repositories.NewsVideos.Interfaces;

/// <summary>
/// Reads news videos from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface INewsVideoReadRepository
{
    /// <summary>
    /// Gets one video of the news item, or null if it doesn't exist or belongs to another news item.
    /// </summary>
    Task<NewsVideo?> GetByIdAsync(int newsId, int videoId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all videos of the news item in display order. Empty if it has none.
    /// </summary>
    Task<IReadOnlyList<NewsVideo>> GetByNewsIdAsync(int newsId, CancellationToken cancellationToken);
}
