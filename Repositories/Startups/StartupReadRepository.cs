using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Startups.Interfaces;

namespace StartupLab.Repositories.Startups;

/// <summary>
/// EF Core implementation of <see cref="IStartupReadRepository"/>.
/// </summary>
public class StartupReadRepository(ApplicationDbContext context) : IStartupReadRepository
{
    // Npgsql sends ESCAPE '' when no escape character is given, so it has to be passed explicitly.
    private const string LikeEscapeCharacter = @"\";

    public async Task<Startup?> GetByIdAsync(int startupId, CancellationToken cancellationToken) =>
        await context.Startups
            .AsNoTracking()
            .SingleOrDefaultAsync(st => st.Id == startupId, cancellationToken);

    public async Task<Startup?> GetDetailsAsync(int startupId, CancellationToken cancellationToken) =>
        await context.Startups
            .AsNoTracking()
            .Include(st => st.UserStartups.Where(us => us.User.IsActive).OrderBy(us => us.UserId))
                .ThenInclude(us => us.User)
                    .ThenInclude(u => u.Photo)
            .Include(st => st.StartupCategories.OrderBy(sc => sc.CategoryId))
                .ThenInclude(sc => sc.Category)
            .Include(st => st.Photos.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Id))
            .Include(st => st.Videos.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Id))
            .Include(st => st.Documents.OrderBy(d => d.DisplayOrder).ThenBy(d => d.Id))
            .Include(st => st.Links.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id))
            .AsSplitQuery()
            .SingleOrDefaultAsync(st => st.Id == startupId, cancellationToken);

    public async Task<IReadOnlyList<Startup>> GetStartupsAsync(bool isActive, string? search, int? categoryId, int? founderId, int skip, int take, CancellationToken cancellationToken) =>
        await WithCardData(Filter(isActive, search, categoryId, founderId))
            .OrderBy(st => st.Name)
            .ThenBy(st => st.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    public async Task<int> CountStartupsAsync(bool isActive, string? search, int? categoryId, int? founderId, CancellationToken cancellationToken) =>
        await Filter(isActive, search, categoryId, founderId).CountAsync(cancellationToken);

    public async Task<IReadOnlyList<Startup>> GetByFounderAsync(int userId, int max, CancellationToken cancellationToken) =>
        await WithCardData(context.Startups.Where(st => st.UserStartups.Any(us => us.UserId == userId)))
            .OrderBy(st => st.Name)
            .ThenBy(st => st.Id)
            .Take(max)
            .ToListAsync(cancellationToken);

    public async Task<bool> IsFounderAsync(int startupId, int userId, CancellationToken cancellationToken) =>
        await context.UserStartups.AnyAsync(us => us.StartupId == startupId && us.UserId == userId && us.User.IsActive, cancellationToken);

    private IQueryable<Startup> Filter(bool isActive, string? search, int? categoryId, int? founderId)
    {
        var query = context.Startups.Where(st => st.IsActive == isActive);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{EscapeLikePattern(search.Trim())}%";
            query = query.Where(st => EF.Functions.ILike(st.Name, pattern, LikeEscapeCharacter));
        }

        if (categoryId is not null)
        {
            query = query.Where(st => st.StartupCategories.Any(sc => sc.CategoryId == categoryId));
        }

        if (founderId is not null)
        {
            query = query.Where(st => st.UserStartups.Any(us => us.UserId == founderId));
        }

        return query;
    }

    private static IQueryable<Startup> WithCardData(IQueryable<Startup> query) =>
        query
            .AsNoTracking()
            .Include(st => st.Photos.Where(p => p.IsPrimary))
            .Include(st => st.StartupCategories.OrderBy(sc => sc.CategoryId))
                .ThenInclude(sc => sc.Category)
            .AsSplitQuery();

    private static string EscapeLikePattern(string value) =>
        value
            .Replace(LikeEscapeCharacter, LikeEscapeCharacter + LikeEscapeCharacter)
            .Replace("%", LikeEscapeCharacter + "%")
            .Replace("_", LikeEscapeCharacter + "_");
}
