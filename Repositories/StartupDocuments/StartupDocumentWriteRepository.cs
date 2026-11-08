using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupDocuments.Interfaces;

namespace StartupLab.Repositories.StartupDocuments;

/// <summary>
/// EF Core implementation of <see cref="IStartupDocumentWriteRepository"/>.
/// </summary>
public class StartupDocumentWriteRepository(ApplicationDbContext context) : IStartupDocumentWriteRepository
{
    public async Task<StartupDocument> AddAsync(int startupId, string addressKey, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var document = new StartupDocument
        {
            StartupId = startupId,
            AddressKey = addressKey,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.StartupDocuments.Add(document);
        await context.SaveChangesAsync(cancellationToken);
        return document;
    }

    public async Task<bool> UpdateAsync(int startupId, int documentId, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.StartupDocuments
            .Where(d => d.Id == documentId && d.StartupId == startupId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Title, title)
                .SetProperty(d => d.Description, description)
                .SetProperty(d => d.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<string?> DeleteAsync(int startupId, int documentId, CancellationToken cancellationToken)
    {
        var document = await context.StartupDocuments.SingleOrDefaultAsync(d => d.Id == documentId && d.StartupId == startupId, cancellationToken);
        if (document is null)
        {
            return null;
        }

        context.StartupDocuments.Remove(document);
        await context.SaveChangesAsync(cancellationToken);
        return document.AddressKey;
    }
}
