using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupVideos.Interfaces;

namespace StartupLab.Repositories.StartupVideos;

/// <summary>
/// EF Core implementation of <see cref="IStartupVideoWriteRepository"/>.
/// </summary>
public class StartupVideoWriteRepository(ApplicationDbContext context) : IStartupVideoWriteRepository
{
    public async Task<StartupVideo> AddAsync(int startupId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var video = new StartupVideo
        {
            StartupId = startupId,
            AddressKey = addressKey,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.StartupVideos.Add(video);
        await context.SaveChangesAsync(cancellationToken);
        return video;
    }

    public async Task<bool> UpdateAsync(int startupId, int videoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.StartupVideos
            .Where(v => v.Id == videoId && v.StartupId == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(v => v.Title, title)
                .SetProperty(v => v.Description, description)
                .SetProperty(v => v.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<string?> DeleteAsync(int startupId, int videoId, CancellationToken cancellationToken)
    {
        var video = await context.StartupVideos.SingleOrDefaultAsync(v => v.Id == videoId && v.StartupId == startupId, cancellationToken);
        if (video is null)
        {
            return null;
        }

        context.StartupVideos.Remove(video);
        await context.SaveChangesAsync(cancellationToken);
        return video.AddressKey;
    }
}
