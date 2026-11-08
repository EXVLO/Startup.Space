using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Users.Interfaces;

/// <summary>
/// Changes and deletes user data: profile, mentor and investor roles, experiences, and the user itself (soft or hard delete).
/// </summary>
public interface IUserWriteRepository
{
    /// <summary>
    /// Updates the user's name, surname and description. Returns false if the user doesn't exist.
    /// </summary>
    Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Gives or removes the mentor role. Returns false if the user doesn't exist.
    /// </summary>
    Task<bool> SetMentorAsync(int userId, bool isMentor, CancellationToken cancellationToken);

    /// <summary>
    /// Marks or unmarks the user as an investor. Returns false if the user doesn't exist.
    /// </summary>
    Task<bool> SetInvestorAsync(int userId, bool isInvestor, CancellationToken cancellationToken);

    /// <summary>
    /// Adds an experience entry to the user and returns it with its new id.
    /// </summary>
    Task<UserExperience> AddExperienceAsync(int userId, string? title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Changes an experience entry. Returns false if it doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> UpdateExperienceAsync(int userId, int experienceId, string? title, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an experience entry. Returns false if it doesn't exist or belongs to another user.
    /// </summary>
    Task<bool> DeleteExperienceAsync(int userId, int experienceId, CancellationToken cancellationToken);

    /// <summary>
    /// Makes an inactive user active again. Their startups stay as they are. Returns false if the user doesn't exist.
    /// </summary>
    Task<bool> ActivateAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Makes the user inactive (soft delete). Their data is kept. Their startups that have no active founders left become inactive too.
    /// Returns false if the user doesn't exist.
    /// </summary>
    Task<bool> DeactivateAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the user for good (hard delete), with their photo, contacts, experiences, checklist progress and founder links.
    /// Their startups that have no active founders left become inactive.
    /// </summary>
    /// <returns>The address key of the removed photo (empty if they had none), or null if the user doesn't exist.</returns>
    Task<IReadOnlyList<string>?> DeleteAsync(int userId, CancellationToken cancellationToken);
}
