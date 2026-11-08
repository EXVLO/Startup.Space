using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Startups;
using StartupLab.Repositories.Users.Interfaces;

namespace StartupLab.Repositories.Users;

/// <summary>
/// EF Core implementation of <see cref="IUserWriteRepository"/>.
/// </summary>
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

    public async Task<bool> SetInvestorAsync(int userId, bool isInvestor, CancellationToken cancellationToken) =>
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.IsInvestor, isInvestor)
                .SetProperty(u => u.DateModified, DateTime.UtcNow)
                .SetProperty(u => u.ConcurrencyStamp, Guid.NewGuid().ToString()),
                cancellationToken) > 0;

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

    public async Task<bool> ActivateAsync(int userId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(userId, true, cancellationToken);

    public async Task<bool> DeactivateAsync(int userId, CancellationToken cancellationToken) =>
        await RemoveFromStartupsAsync(userId, () => UpdateIsActiveAsync(userId, false, cancellationToken), cancellationToken);

    public async Task<IReadOnlyList<string>?> DeleteAsync(int userId, CancellationToken cancellationToken)
    {
        var addressKeys = await context.UserPhotos
            .Where(p => p.UserId == userId)
            .Select(p => p.AddressKey)
            .ToListAsync(cancellationToken);

        var deleted = await RemoveFromStartupsAsync(
            userId,
            async () => await context.Users
                .Where(u => u.Id == userId)
                .ExecuteDeleteAsync(cancellationToken) > 0,
            cancellationToken);

        return deleted ? addressKeys : null;
    }

    /// <summary>
    /// Runs <paramref name="change"/> in a transaction with the user's startups locked, then makes the startups
    /// that have no active founders left inactive. Nothing is saved if <paramref name="change"/> returns false.
    /// </summary>
    private async Task<bool> RemoveFromStartupsAsync(int userId, Func<Task<bool>> change, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var startupIds = await context.UserStartups
            .Where(us => us.UserId == userId)
            .Select(us => us.StartupId)
            .ToListAsync(cancellationToken);
        await StartupFounderRemoval.LockAsync(context, startupIds, cancellationToken);

        if (!await change())
        {
            return false;
        }

        await StartupFounderRemoval.DeactivateWithoutFoundersAsync(context, startupIds, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return true;
    }

    private async Task<bool> UpdateIsActiveAsync(int userId, bool isActive, CancellationToken cancellationToken) =>
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.IsActive, isActive)
                .SetProperty(u => u.DateModified, DateTime.UtcNow)
                .SetProperty(u => u.ConcurrencyStamp, Guid.NewGuid().ToString()),
                cancellationToken) > 0;
}
