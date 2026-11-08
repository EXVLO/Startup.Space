using Microsoft.EntityFrameworkCore;
using StartupLab.Data;

namespace StartupLab.Repositories.Startups;

/// <summary>
/// Shared steps for removing founders, used when a founder leaves a startup and when a user is made inactive or deleted.
/// A startup with no active founders left becomes inactive.
/// </summary>
internal static class StartupFounderRemoval
{
    /// <summary>
    /// Locks the startups until the transaction ends and sets their DateModified. Call it before founders are removed.
    /// </summary>
    // Without the lock, two founders leaving at the same time could both still see the other one and leave the startup active with no active founders.
    public static async Task LockAsync(ApplicationDbContext context, IReadOnlyCollection<int> startupIds, CancellationToken cancellationToken) =>
        await context.Startups
            .Where(st => startupIds.Contains(st.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(st => st.DateModified, DateTime.UtcNow),
                cancellationToken);

    /// <summary>
    /// Makes every startup in <paramref name="startupIds"/> that has no active founders left inactive.
    /// </summary>
    public static async Task DeactivateWithoutFoundersAsync(ApplicationDbContext context, IReadOnlyCollection<int> startupIds, CancellationToken cancellationToken) =>
        await context.Startups
            .Where(st => startupIds.Contains(st.Id) && st.IsActive && !st.UserStartups.Any(us => us.User.IsActive))
            .ExecuteUpdateAsync(s => s
                .SetProperty(st => st.IsActive, false)
                .SetProperty(st => st.DateModified, DateTime.UtcNow),
                cancellationToken);
}
