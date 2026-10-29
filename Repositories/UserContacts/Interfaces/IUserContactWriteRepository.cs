using StartupLab.Models.Entities;

namespace StartupLab.Repositories.UserContacts.Interfaces;

/// <summary>
/// Adds, changes and deletes user contacts. A user can have many.
/// </summary>
public interface IUserContactWriteRepository
{
    /// <summary>
    /// Adds a contact to the user and returns it with its new id.
    /// </summary>
    Task<UserContact> AddAsync(int userId, string contactInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a contact. Returns false if the contact doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> UpdateAsync(int userId, int contactId, string contactInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a contact. Returns false if the contact doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> DeleteAsync(int userId, int contactId, CancellationToken cancellationToken);
}
