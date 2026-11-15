using StartupLab.Models.Entities;

namespace StartupLab.Repositories.NewsPhotos.Interfaces;

/// <summary>
/// Reads news photos from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface INewsPhotoReadRepository
{
    /// <summary>
    /// Gets one photo of the news item, or null if it doesn't exist or belongs to another news item.
    /// </summary>
    Task<NewsPhoto?> GetByIdAsync(int newsId, int photoId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all photos of the news item in display order. Empty if it has none.
    /// </summary>
    Task<IReadOnlyList<NewsPhoto>> GetByNewsIdAsync(int newsId, CancellationToken cancellationToken);
}
