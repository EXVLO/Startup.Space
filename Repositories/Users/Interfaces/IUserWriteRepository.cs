using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Users.Interfaces;

public interface IUserWriteRepository
{
    Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? description, CancellationToken cancellationToken);

    Task<bool> SetMentorAsync(int userId, bool isMentor, CancellationToken cancellationToken);

    /// <returns>The previous photo's address key, or null if the user had no photo.</returns>
    Task<string?> SetPhotoAsync(int userId, string addressKey, CancellationToken cancellationToken);

    /// <returns>The removed photo's address key, or null if the user had no photo.</returns>
    Task<string?> RemovePhotoAsync(int userId, CancellationToken cancellationToken);

    Task<UserContact> AddContactAsync(int userId, string contactInfo, CancellationToken cancellationToken);

    Task<bool> UpdateContactAsync(int userId, int contactId, string contactInfo, CancellationToken cancellationToken);

    Task<bool> DeleteContactAsync(int userId, int contactId, CancellationToken cancellationToken);

    Task<UserExperience> AddExperienceAsync(int userId, string? title, string? description, CancellationToken cancellationToken);

    Task<bool> UpdateExperienceAsync(int userId, int experienceId, string? title, string? description, CancellationToken cancellationToken);

    Task<bool> DeleteExperienceAsync(int userId, int experienceId, CancellationToken cancellationToken);
}
