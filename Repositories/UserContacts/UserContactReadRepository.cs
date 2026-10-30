using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.UserContacts.Interfaces;

namespace StartupLab.Repositories.UserContacts;

/// <summary>
/// EF Core implementation of <see cref="IUserContactReadRepository"/>.
/// </summary>
public class UserContactReadRepository(ApplicationDbContext context) : IUserContactReadRepository
{
    public Task<UserContact?> GetByIdAsync(int userId, int contactId, CancellationToken cancellationToken) =>
        context.UserContacts
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == contactId && c.UserId == userId, cancellationToken);

    public async Task<IReadOnlyList<UserContact>> GetByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        await context.UserContacts.AsNoTracking().Where(uc => uc.UserId == userId).OrderBy(uc => uc.Id).AsNoTracking().ToListAsync(cancellationToken);
}
