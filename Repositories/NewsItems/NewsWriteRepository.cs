using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.NewsItems.Interfaces;

namespace StartupLab.Repositories.NewsItems;

/// <summary>
/// EF Core implementation of <see cref="INewsWriteRepository"/>.
/// </summary>
public class NewsWriteRepository(ApplicationDbContext context) : INewsWriteRepository
{
    public async Task<News> CreateAsync(string title, string? description, CancellationToken cancellationToken)
    {
        var news = new News
        {
            Title = title,
            Description = description,
            DateCreated = DateTime.UtcNow
        };

        context.News.Add(news);
        await context.SaveChangesAsync(cancellationToken);
        return news;
    }

    public async Task<bool> UpdateAsync(int newsId, string title, string? description, CancellationToken cancellationToken) =>
        await context.News
            .Where(n => n.Id == newsId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.Title, title)
                .SetProperty(n => n.Description, description)
                .SetProperty(n => n.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> SetPrimaryAsync(int newsId, CancellationToken cancellationToken)
    {
        if (!await context.News.AnyAsync(n => n.Id == newsId, cancellationToken))
        {
            return false;
        }

        await context.News
            .Where(n => n.IsPrimary || n.Id == newsId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.IsPrimary, n => n.Id == newsId)
                .SetProperty(n => n.DateModified, DateTime.UtcNow),
                cancellationToken);
        return true;
    }

    public async Task<bool> SetTagsAsync(int newsId, IReadOnlyCollection<int> tagIds, CancellationToken cancellationToken)
    {
        if (!await context.News.AnyAsync(n => n.Id == newsId, cancellationToken))
        {
            return false;
        }

        var newTagIds = await context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync(cancellationToken);

        var current = await context.NewsTags
            .Where(nt => nt.NewsId == newsId)
            .ToListAsync(cancellationToken);

        context.NewsTags.RemoveRange(current.Where(nt => !newTagIds.Contains(nt.TagId)));
        context.NewsTags.AddRange(newTagIds
            .Except(current.Select(nt => nt.TagId))
            .Select(tagId => new NewsTag { NewsId = newsId, TagId = tagId }));

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<string>?> DeleteAsync(int newsId, CancellationToken cancellationToken)
    {
        var addressKeys = await context.NewsPhotos
            .Where(p => p.NewsId == newsId)
            .Select(p => p.AddressKey)
            .Concat(context.NewsVideos
                .Where(v => v.NewsId == newsId)
                .Select(v => v.AddressKey))
            .ToListAsync(cancellationToken);

        var deleted = await context.News
            .Where(n => n.Id == newsId)
            .ExecuteDeleteAsync(cancellationToken) > 0;

        return deleted ? addressKeys : null;
    }
}
