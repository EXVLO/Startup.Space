using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Startups.Interfaces;

/// <summary>
/// Reads startups, their founders and their categories. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IStartupReadRepository
{
    /// <summary>
    /// Gets a startup by id, or null if not found.
    /// </summary>
    Task<Startup?> GetByIdAsync(int startupId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a startup with its active founders, categories, photos, videos, documents and links, or null if not found.
    /// </summary>
    Task<Startup?> GetDetailsAsync(int startupId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets one page of active or inactive startups with their primary photo and categories, sorted by name.
    /// <paramref name="search"/> matches part of the name, ignoring case. Null filters are not applied.
    /// </summary>
    Task<IReadOnlyList<Startup>> GetStartupsAsync(bool isActive, string? search, int? categoryId, int? founderId, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Counts the startups that <see cref="GetStartupsAsync"/> would return with the same filters. Used for paging.
    /// </summary>
    Task<int> CountStartupsAsync(bool isActive, string? search, int? categoryId, int? founderId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets up to <paramref name="max"/> startups the user founded, active and inactive, with their primary photo and categories, sorted by name.
    /// </summary>
    Task<IReadOnlyList<Startup>> GetByFounderAsync(int userId, int max, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether the user is one of the startup's founders and is active.
    /// </summary>
    Task<bool> IsFounderAsync(int startupId, int userId, CancellationToken cancellationToken);
}
