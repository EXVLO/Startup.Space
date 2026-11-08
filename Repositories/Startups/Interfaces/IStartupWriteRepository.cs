using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Startups.Interfaces;

/// <summary>
/// Adds, changes and deletes (soft or hard) startups, and changes their founders and categories.
/// </summary>
public interface IStartupWriteRepository
{
    /// <summary>
    /// Adds a startup with the user as its first founder and returns it with its new id.
    /// </summary>
    Task<Startup> CreateAsync(int userId, string name, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the startup's name and description. Returns false if the startup doesn't exist.
    /// </summary>
    Task<bool> UpdateAsync(int startupId, string name, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Makes an inactive startup active again, so it leaves the Archive. Returns false if the startup doesn't exist.
    /// </summary>
    Task<bool> ActivateAsync(int startupId, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the startup inactive (soft delete). Its data is kept and it is shown in the Archive.
    /// Returns false if the startup doesn't exist.
    /// </summary>
    Task<bool> DeactivateAsync(int startupId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the startup for good (hard delete), with its photos, videos, documents, links, founder links, categories and checklist progress.
    /// </summary>
    /// <returns>The address keys of the removed photos, videos and documents, or null if the startup doesn't exist.</returns>
    Task<IReadOnlyList<string>?> DeleteAsync(int startupId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds the user as a founder of the startup. Does nothing if they already are one.
    /// Returns false if the startup doesn't exist, or the user doesn't exist or is inactive.
    /// </summary>
    Task<bool> AddFounderAsync(int startupId, int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Removes the user from the startup's founders. If no active founders are left, the startup becomes inactive.
    /// Returns false if they weren't a founder.
    /// </summary>
    Task<bool> RemoveFounderAsync(int startupId, int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Replaces the startup's categories with <paramref name="categoryIds"/>. Ids of categories that don't exist are skipped.
    /// Returns false if the startup doesn't exist.
    /// </summary>
    Task<bool> SetCategoriesAsync(int startupId, IReadOnlyCollection<int> categoryIds, CancellationToken cancellationToken);
}
