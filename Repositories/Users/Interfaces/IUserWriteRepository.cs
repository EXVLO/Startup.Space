using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Users.Interfaces;

/// <summary>
/// Changes user data: profile, mentor role and experiences.
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
}
