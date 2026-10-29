using StartupLab.Models.Entities;

namespace StartupLab.Repositories.UserContacts.Interfaces;

/// <summary>
/// Reads user contacts from the database. Results are not tracked, so changing them saves nothing.
/// </summary>
public interface IUserContactReadRepository
{
    /// <summary>
    /// Gets one contact of the user, or null if it doesn't exist or belongs to another user.
    /// </summary>
    Task<UserContact?> GetByIdAsync(int userId, int contactId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all contacts of the user, oldest first. Empty if the user has none.
    /// </summary>
    Task<IReadOnlyList<UserContact>> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
}
