using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitDocuments.Interfaces;

namespace StartupLab.Repositories.ToolkitDocuments;

/// <summary>
/// EF Core implementation of <see cref="IToolkitDocumentReadRepository"/>.
/// </summary>
public class ToolkitDocumentReadRepository(ApplicationDbContext context) : IToolkitDocumentReadRepository
{
    public async Task<ToolkitDocument?> GetByIdAsync(int toolkitDocumentId, CancellationToken cancellationToken) =>
        await context.ToolkitDocuments
            .AsNoTracking()
            .SingleOrDefaultAsync(d => d.Id == toolkitDocumentId, cancellationToken);

    public async Task<IReadOnlyList<ToolkitDocument>> GetDocumentsAsync(int max, CancellationToken cancellationToken) =>
        await context.ToolkitDocuments
            .AsNoTracking()
            .OrderBy(d => d.DisplayOrder)
            .ThenBy(d => d.Id)
            .Take(max)
            .ToListAsync(cancellationToken);
}
