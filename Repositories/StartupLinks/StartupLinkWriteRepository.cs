using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupLinks.Interfaces;

namespace StartupLab.Repositories.StartupLinks;

/// <summary>
/// EF Core implementation of <see cref="IStartupLinkWriteRepository"/>.
/// </summary>
public class StartupLinkWriteRepository(ApplicationDbContext context) : IStartupLinkWriteRepository
{
    public async Task<StartupLink> AddAsync(int startupId, string link, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var startupLink = new StartupLink
        {
            StartupId = startupId,
            Link = link,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.StartupLinks.Add(startupLink);
        await context.SaveChangesAsync(cancellationToken);
        return startupLink;
    }

    public async Task<bool> UpdateAsync(int startupId, int linkId, string link, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.StartupLinks
            .Where(l => l.Id == linkId && l.StartupId == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(l => l.Link, link)
                .SetProperty(l => l.Title, title)
                .SetProperty(l => l.Description, description)
                .SetProperty(l => l.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<bool> DeleteAsync(int startupId, int linkId, CancellationToken cancellationToken) =>
        await context.StartupLinks
            .Where(l => l.Id == linkId && l.StartupId == startupId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
}
