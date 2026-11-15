using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.NewsVideos.Interfaces;

namespace StartupLab.Repositories.NewsVideos;

/// <summary>
/// EF Core implementation of <see cref="INewsVideoWriteRepository"/>.
/// </summary>
public class NewsVideoWriteRepository(ApplicationDbContext context) : INewsVideoWriteRepository
{
    public async Task<NewsVideo> AddAsync(int newsId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var video = new NewsVideo
        {
            NewsId = newsId,
            AddressKey = addressKey,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.NewsVideos.Add(video);
        await context.SaveChangesAsync(cancellationToken);
        return video;
    }

    public async Task<bool> UpdateAsync(int newsId, int videoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.NewsVideos
            .Where(v => v.Id == videoId && v.NewsId == newsId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(v => v.Title, title)
                .SetProperty(v => v.Description, description)
                .SetProperty(v => v.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<bool> SetPrimaryAsync(int newsId, int videoId, CancellationToken cancellationToken)
    {
        if (!await context.NewsVideos.AnyAsync(v => v.Id == videoId && v.NewsId == newsId, cancellationToken))
        {
            return false;
        }

        await context.NewsVideos
            .Where(v => v.NewsId == newsId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(v => v.IsPrimary, v => v.Id == videoId),
                cancellationToken);
        return true;
    }

    public async Task<string?> DeleteAsync(int newsId, int videoId, CancellationToken cancellationToken)
    {
        var video = await context.NewsVideos.SingleOrDefaultAsync(v => v.Id == videoId && v.NewsId == newsId, cancellationToken);
        if (video is null)
        {
            return null;
        }

        context.NewsVideos.Remove(video);
        await context.SaveChangesAsync(cancellationToken);
        return video.AddressKey;
    }
}
