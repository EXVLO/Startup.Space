using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Users.Interfaces;

namespace StartupLab.Repositories.Users;

/// <summary>
/// EF Core implementation of <see cref="IUserReadRepository"/>.
/// </summary>
public class UserReadRepository(ApplicationDbContext context) : IUserReadRepository
{
    public Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken) =>
        context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);

    public Task<User?> GetProfileAsync(int userId, CancellationToken cancellationToken) =>
        context.Users
            .AsNoTracking()
            .Include(u => u.Photo)
            .Include(u => u.Contacts.OrderBy(c => c.Id))
            .Include(u => u.Experiences.OrderBy(e => e.Id))
            .AsSplitQuery()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);

    public async Task<IReadOnlyList<User>> GetUsersAsync(bool isActive, int skip, int take, CancellationToken cancellationToken) =>
        await context.Users
            .AsNoTracking()
            .Include(u => u.Photo)
            .Where(u => u.IsActive == isActive)
            .OrderBy(u => u.Name)
            .ThenBy(u => u.Surname)
            .ThenBy(u => u.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    public Task<int> CountUsersAsync(bool isActive, CancellationToken cancellationToken) =>
        context.Users.CountAsync(u => u.IsActive == isActive, cancellationToken);

    public async Task<IReadOnlyList<User>> GetMentorsAsync(int max, CancellationToken cancellationToken) =>
        await context.Users
            .AsNoTracking()
            .Include(u => u.Photo)
            .Where(u => u.IsActive && u.IsMentor)
            .OrderBy(u => u.Name)
            .ThenBy(u => u.Surname)
            .ThenBy(u => u.Id)
            .Take(max)
            .ToListAsync(cancellationToken);
}
