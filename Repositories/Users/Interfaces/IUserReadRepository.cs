using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Users.Interfaces;

/// <summary>
/// Reads users from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IUserReadRepository
{
    /// <summary>
    /// Gets a user by id, or null if not found.
    /// </summary>
    Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user with their photo, contacts and experiences, or null if not found.
    /// </summary>
    Task<User?> GetProfileAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets one page of active or inactive users with their photo, sorted by name and surname.
    /// </summary>
    Task<IReadOnlyList<User>> GetUsersAsync(bool isActive, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Counts active or inactive users. Used together with <see cref="GetUsersAsync"/> for paging.
    /// </summary>
    Task<int> CountUsersAsync(bool isActive, CancellationToken cancellationToken);

    /// <summary>
    /// Gets up to <paramref name="max"/> active mentors with their photo, sorted by name and surname.
    /// </summary>
    Task<IReadOnlyList<User>> GetMentorsAsync(int max, CancellationToken cancellationToken);
}
