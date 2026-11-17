using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitVideos.Interfaces;

namespace StartupLab.Repositories.ToolkitVideos;

/// <summary>
/// EF Core implementation of <see cref="IToolkitVideoWriteRepository"/>.
/// </summary>
public class ToolkitVideoWriteRepository(ApplicationDbContext context) : IToolkitVideoWriteRepository
{
    public async Task<ToolkitVideo> CreateAsync(string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var video = new ToolkitVideo
        {
            AddressKey = addressKey,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.ToolkitVideos.Add(video);
        await context.SaveChangesAsync(cancellationToken);
        return video;
    }

    public async Task<bool> UpdateAsync(int toolkitVideoId, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.ToolkitVideos
            .Where(v => v.Id == toolkitVideoId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(v => v.Title, title)
                .SetProperty(v => v.Description, description)
                .SetProperty(v => v.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<string?> DeleteAsync(int toolkitVideoId, CancellationToken cancellationToken)
    {
        var video = await context.ToolkitVideos.SingleOrDefaultAsync(v => v.Id == toolkitVideoId, cancellationToken);
        if (video is null)
        {
            return null;
        }

        context.ToolkitVideos.Remove(video);
        await context.SaveChangesAsync(cancellationToken);
        return video.AddressKey;
    }
}
