using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitDocuments.Interfaces;

namespace StartupLab.Repositories.ToolkitDocuments;

/// <summary>
/// EF Core implementation of <see cref="IToolkitDocumentWriteRepository"/>.
/// </summary>
public class ToolkitDocumentWriteRepository(ApplicationDbContext context) : IToolkitDocumentWriteRepository
{
    public async Task<ToolkitDocument> CreateAsync(string url, string? title, string? description, int displayOrder, CancellationToken cancellationToken)
    {
        var document = new ToolkitDocument
        {
            Url = url,
            Title = title,
            Description = description,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.ToolkitDocuments.Add(document);
        await context.SaveChangesAsync(cancellationToken);
        return document;
    }

    public async Task<bool> UpdateAsync(int toolkitDocumentId, string url, string? title, string? description, int displayOrder, CancellationToken cancellationToken) =>
        await context.ToolkitDocuments
            .Where(d => d.Id == toolkitDocumentId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Url, url)
                .SetProperty(d => d.Title, title)
                .SetProperty(d => d.Description, description)
                .SetProperty(d => d.DisplayOrder, displayOrder),
                cancellationToken) > 0;

    public async Task<bool> DeleteAsync(int toolkitDocumentId, CancellationToken cancellationToken) =>
        await context.ToolkitDocuments
            .Where(d => d.Id == toolkitDocumentId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
}
