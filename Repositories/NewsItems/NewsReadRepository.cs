using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.NewsItems.Interfaces;

namespace StartupLab.Repositories.NewsItems;

/// <summary>
/// EF Core implementation of <see cref="INewsReadRepository"/>.
/// </summary>
public class NewsReadRepository(ApplicationDbContext context) : INewsReadRepository
{
    public async Task<News?> GetByIdAsync(int newsId, CancellationToken cancellationToken) =>
        await context.News
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == newsId, cancellationToken);

    public async Task<News?> GetDetailsAsync(int newsId, CancellationToken cancellationToken) =>
        await context.News
            .AsNoTracking()
            .Include(n => n.Photos.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Id))
            .Include(n => n.Videos.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Id))
            .Include(n => n.NewsTags.OrderBy(nt => nt.TagId))
                .ThenInclude(nt => nt.Tag)
            .AsSplitQuery()
            .SingleOrDefaultAsync(n => n.Id == newsId, cancellationToken);

    public async Task<News?> GetPrimaryAsync(CancellationToken cancellationToken) =>
        await WithCardData(context.News.Where(n => n.IsPrimary))
            .OrderBy(n => n.Id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<News>> GetNewsAsync(int? tagId, int skip, int take, CancellationToken cancellationToken) =>
        await WithCardData(Filter(tagId))
            .OrderByDescending(n => n.DateCreated)
            .ThenByDescending(n => n.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    public async Task<int> CountNewsAsync(int? tagId, CancellationToken cancellationToken) =>
        await Filter(tagId).CountAsync(cancellationToken);

    private IQueryable<News> Filter(int? tagId)
    {
        var query = context.News.AsQueryable();

        if (tagId is not null)
        {
            query = query.Where(n => n.NewsTags.Any(nt => nt.TagId == tagId));
        }

        return query;
    }

    private static IQueryable<News> WithCardData(IQueryable<News> query) =>
        query
            .AsNoTracking()
            .Include(n => n.Photos.Where(p => p.IsPrimary))
            .Include(n => n.NewsTags.OrderBy(nt => nt.TagId))
                .ThenInclude(nt => nt.Tag)
            .AsSplitQuery();
}
