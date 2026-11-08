using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupPhotos.Interfaces;

/// <summary>
/// Reads startup photos from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IStartupPhotoReadRepository
{
    /// <summary>
    /// Gets one photo of the startup, or null if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<StartupPhoto?> GetByIdAsync(int startupId, int photoId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all photos of the startup in display order. Empty if it has none.
    /// </summary>
    Task<IReadOnlyList<StartupPhoto>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken);
}
