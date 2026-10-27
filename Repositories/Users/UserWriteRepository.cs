using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Users.Interfaces;

namespace StartupLab.Repositories.Users;

public class UserWriteRepository(ApplicationDbContext context) : IUserWriteRepository
{
    // ConcurrencyStamp is refreshed because Identity's UserManager uses it to detect conflicting updates.
    public async Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? description, CancellationToken cancellationToken) =>
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, name)
                .SetProperty(u => u.Surname, surname)
                .SetProperty(u => u.Description, description)
                .SetProperty(u => u.DateModified, DateTime.UtcNow)
                .SetProperty(u => u.ConcurrencyStamp, Guid.NewGuid().ToString()),
                cancellationToken) > 0;

    public async Task<bool> SetMentorAsync(int userId, bool isMentor, CancellationToken cancellationToken) =>
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.IsMentor, isMentor)
                .SetProperty(u => u.DateModified, DateTime.UtcNow)
                .SetProperty(u => u.ConcurrencyStamp, Guid.NewGuid().ToString()),
                cancellationToken) > 0;

    public async Task<string?> SetPhotoAsync(int userId, string addressKey, CancellationToken cancellationToken)
    {
        var photo = await context.UserPhotos.SingleOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        var previousAddressKey = photo?.AddressKey;

        if (photo is null)
        {
            photo = new UserPhoto { UserId = userId };
            context.UserPhotos.Add(photo);
        }

        photo.AddressKey = addressKey;
        photo.DateCreated = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return previousAddressKey;
    }

    public async Task<string?> RemovePhotoAsync(int userId, CancellationToken cancellationToken)
    {
        var photo = await context.UserPhotos.SingleOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        if (photo is null)
        {
            return null;
        }

        context.UserPhotos.Remove(photo);
        await context.SaveChangesAsync(cancellationToken);
        return photo.AddressKey;
    }

    public async Task<UserContact> AddContactAsync(int userId, string contactInfo, CancellationToken cancellationToken)
    {
        var contact = new UserContact
        {
            UserId = userId,
            ContactInfo = contactInfo,
            DateCreated = DateTime.UtcNow
        };

        context.UserContacts.Add(contact);
        await context.SaveChangesAsync(cancellationToken);
        return contact;
    }

    public async Task<bool> UpdateContactAsync(int userId, int contactId, string contactInfo, CancellationToken cancellationToken) =>
        await context.UserContacts
            .Where(c => c.Id == contactId && c.UserId == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.ContactInfo, contactInfo)
                .SetProperty(c => c.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> DeleteContactAsync(int userId, int contactId, CancellationToken cancellationToken) =>
        await context.UserContacts
            .Where(c => c.Id == contactId && c.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken) > 0;

    public async Task<UserExperience> AddExperienceAsync(int userId, string? title, string? description, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var experience = new UserExperience
        {
            UserId = userId,
            Title = title,
            Description = description,
            DateCreated = now,
            DateModified = now
        };

        context.UserExperiences.Add(experience);
        await context.SaveChangesAsync(cancellationToken);
        return experience;
    }

    public async Task<bool> UpdateExperienceAsync(int userId, int experienceId, string? title, string? description, CancellationToken cancellationToken) =>
        await context.UserExperiences
            .Where(e => e.Id == experienceId && e.UserId == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.Title, title)
                .SetProperty(e => e.Description, description)
                .SetProperty(e => e.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> DeleteExperienceAsync(int userId, int experienceId, CancellationToken cancellationToken) =>
        await context.UserExperiences
            .Where(e => e.Id == experienceId && e.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
}
