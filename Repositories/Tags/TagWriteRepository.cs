using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Tags.Interfaces;

namespace StartupLab.Repositories.Tags;

/// <summary>
/// EF Core implementation of <see cref="ITagWriteRepository"/>.
/// </summary>
public class TagWriteRepository(ApplicationDbContext context) : ITagWriteRepository
{
    public async Task<Tag> CreateAsync(string title, string? description, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Title = title,
            Description = description,
            DateCreated = DateTime.UtcNow
        };

        context.Tags.Add(tag);
        await context.SaveChangesAsync(cancellationToken);
        return tag;
    }

    public async Task<bool> UpdateAsync(int tagId, string title, string? description, CancellationToken cancellationToken) =>
        await context.Tags
            .Where(t => t.Id == tagId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Title, title)
                .SetProperty(t => t.Description, description)
                .SetProperty(t => t.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> ActivateAsync(int tagId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(tagId, true, cancellationToken);

    public async Task<bool> DeactivateAsync(int tagId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(tagId, false, cancellationToken);

    private async Task<bool> UpdateIsActiveAsync(int tagId, bool isActive, CancellationToken cancellationToken) =>
        await context.Tags
            .Where(t => t.Id == tagId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.IsActive, isActive)
                .SetProperty(t => t.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;
}
