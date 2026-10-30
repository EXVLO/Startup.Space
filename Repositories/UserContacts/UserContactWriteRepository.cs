using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.UserContacts.Interfaces;

namespace StartupLab.Repositories.UserContacts;

/// <summary>
/// EF Core implementation of <see cref="IUserContactWriteRepository"/>.
/// </summary>
public class UserContactWriteRepository(ApplicationDbContext context) : IUserContactWriteRepository
{
    public async Task<UserContact> AddAsync(int userId, string contactInfo, CancellationToken cancellationToken)
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

    public async Task<bool> UpdateAsync(int userId, int contactId, string contactInfo, CancellationToken cancellationToken) =>
        await context.UserContacts
            .Where(c => c.Id == contactId && c.UserId == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.ContactInfo, contactInfo)
                .SetProperty(c => c.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> DeleteAsync(int userId, int contactId, CancellationToken cancellationToken) =>
        await context.UserContacts
            .Where(c => c.Id == contactId && c.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
}
