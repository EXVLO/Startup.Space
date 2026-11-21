using StartupLab.Models.Entities;
using StartupLab.Services.Users;

namespace StartupLab.Services.Users.Interfaces;

/// <summary>
/// Profile, photo, contacts, experience, and account removal for one user.
/// Mentor and investor roles stay on the admin service.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets the user with their photo, contacts and experiences, or null if not found.
    /// </summary>
    Task<User?> GetProfileAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the user's name, surname and description. Returns false if the user doesn't exist, or the name or surname is blank.
    /// </summary>
    Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the user's photo. Saved is false if the user doesn't exist or the address key is blank.
    /// PreviousAddressKey is the replaced file, or null if the user had no photo.
    /// </summary>
    Task<PhotoChange> SetPhotoAsync(int userId, string addressKey, CancellationToken cancellationToken);

    /// <summary>
    /// Removes the user's photo.
    /// </summary>
    /// <returns>The removed photo's address key, or null if the user had none.</returns>
    Task<string?> DeletePhotoAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a contact and returns it, or null if the user doesn't exist or the text is blank.
    /// </summary>
    Task<UserContact?> AddContactAsync(int userId, string contactInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a contact. Returns false if it doesn't exist, belongs to another user, or the text is blank.
    /// </summary>
    Task<bool> UpdateContactAsync(int userId, int contactId, string contactInfo, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a contact. Returns false if it doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> DeleteContactAsync(int userId, int contactId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds an experience entry and returns it, or null if the user doesn't exist.
    /// </summary>
    Task<UserExperience?> AddExperienceAsync(int userId, string? title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes an experience entry. Returns false if it doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> UpdateExperienceAsync(int userId, int experienceId, string? title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an experience entry. Returns false if it doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> DeleteExperienceAsync(int userId, int experienceId, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the user inactive (soft delete). Startups left with no active founders become inactive too.
    /// Returns false if the user doesn't exist.
    /// </summary>
    Task<bool> DeactivateAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the user for good (hard delete). Startups left with no active founders become inactive too.
    /// </summary>
    /// <returns>The address key of the removed photo (empty if they had none), or null if the user doesn't exist.</returns>
    Task<IReadOnlyList<string>?> DeleteAsync(int userId, CancellationToken cancellationToken);
}
