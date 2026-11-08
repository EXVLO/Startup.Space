using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupLinks.Interfaces;

/// <summary>
/// Adds, changes and deletes startup links. A startup can have many.
/// </summary>
public interface IStartupLinkWriteRepository
{
    /// <summary>
    /// Adds a link to the startup and returns it with its new id.
    /// </summary>
    Task<StartupLink> AddAsync(int startupId, string link, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a link's URL, title, description and position. Returns false if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<bool> UpdateAsync(int startupId, int linkId, string link, string? title, string? description, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a link. Returns false if it doesn't exist or belongs to another startup.
    /// </summary>
    Task<bool> DeleteAsync(int startupId, int linkId, CancellationToken cancellationToken);
}
