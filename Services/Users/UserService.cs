using StartupLab.Models.Entities;
using StartupLab.Repositories.UserContacts.Interfaces;
using StartupLab.Repositories.UserPhotos.Interfaces;
using StartupLab.Repositories.Users.Interfaces;
using StartupLab.Services.Users.Interfaces;

namespace StartupLab.Services.Users;

/// <summary>
/// Implementation of <see cref="IUserService"/>.
/// </summary>
public class UserService(
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IUserPhotoWriteRepository userPhotoWriteRepository,
    IUserContactWriteRepository userContactWriteRepository) : IUserService
{
    public async Task<User?> GetProfileAsync(int userId, CancellationToken cancellationToken) =>
        await userReadRepository.GetProfileAsync(userId, cancellationToken);

    public async Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? description, CancellationToken cancellationToken)
    {
        name = name.Trim();
        surname = surname.Trim();
        if (name.Length == 0 || surname.Length == 0)
        {
            return false;
        }

        var normalizedDescription = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        return await userWriteRepository.UpdateProfileAsync(userId, name, surname, normalizedDescription, cancellationToken);
    }

    public async Task<PhotoChange> SetPhotoAsync(int userId, string addressKey, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(addressKey) || await userReadRepository.GetByIdAsync(userId, cancellationToken) is null)
        {
            return new PhotoChange(false, null);
        }

        var previousAddressKey = await userPhotoWriteRepository.SetByUserIdAsync(userId, addressKey.Trim(), cancellationToken);
        return new PhotoChange(true, previousAddressKey);
    }

    public async Task<string?> DeletePhotoAsync(int userId, CancellationToken cancellationToken) =>
        await userPhotoWriteRepository.DeleteByUserIdAsync(userId, cancellationToken);

    public async Task<UserContact?> AddContactAsync(int userId, string contactInfo, CancellationToken cancellationToken)
    {
        contactInfo = contactInfo.Trim();
        if (contactInfo.Length == 0 || await userReadRepository.GetByIdAsync(userId, cancellationToken) is null)
        {
            return null;
        }

        return await userContactWriteRepository.AddAsync(userId, contactInfo, cancellationToken);
    }

    public async Task<bool> UpdateContactAsync(int userId, int contactId, string contactInfo, CancellationToken cancellationToken)
    {
        contactInfo = contactInfo.Trim();
        if (contactInfo.Length == 0)
        {
            return false;
        }

        return await userContactWriteRepository.UpdateAsync(userId, contactId, contactInfo, cancellationToken);
    }

    public async Task<bool> DeleteContactAsync(int userId, int contactId, CancellationToken cancellationToken) =>
        await userContactWriteRepository.DeleteAsync(userId, contactId, cancellationToken);

    public async Task<UserExperience?> AddExperienceAsync(int userId, string? title, string? description, CancellationToken cancellationToken)
    {
        if (await userReadRepository.GetByIdAsync(userId, cancellationToken) is null)
        {
            return null;
        }

        return await userWriteRepository.AddExperienceAsync(userId, Normalize(title), Normalize(description), cancellationToken);
    }

    public async Task<bool> UpdateExperienceAsync(int userId, int experienceId, string? title, string? description, CancellationToken cancellationToken) =>
        await userWriteRepository.UpdateExperienceAsync(userId, experienceId, Normalize(title), Normalize(description), cancellationToken);

    public async Task<bool> DeleteExperienceAsync(int userId, int experienceId, CancellationToken cancellationToken) =>
        await userWriteRepository.DeleteExperienceAsync(userId, experienceId, cancellationToken);

    public async Task<bool> DeactivateAsync(int userId, CancellationToken cancellationToken) =>
        await userWriteRepository.DeactivateAsync(userId, cancellationToken);

    public async Task<IReadOnlyList<string>?> DeleteAsync(int userId, CancellationToken cancellationToken) =>
        await userWriteRepository.DeleteAsync(userId, cancellationToken);

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
