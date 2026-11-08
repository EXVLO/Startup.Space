using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupDocuments.Interfaces;

namespace StartupLab.Repositories.StartupDocuments;

/// <summary>
/// EF Core implementation of <see cref="IStartupDocumentReadRepository"/>.
/// </summary>
public class StartupDocumentReadRepository(ApplicationDbContext context) : IStartupDocumentReadRepository
{
    public async Task<StartupDocument?> GetByIdAsync(int startupId, int documentId, CancellationToken cancellationToken) =>
        await context.StartupDocuments
            .AsNoTracking()
            .SingleOrDefaultAsync(d => d.Id == documentId && d.StartupId == startupId, cancellationToken);

    public async Task<IReadOnlyList<StartupDocument>> GetByStartupIdAsync(int startupId, CancellationToken cancellationToken) =>
        await context.StartupDocuments
            .AsNoTracking()
            .Where(d => d.StartupId == startupId)
            .OrderBy(d => d.DisplayOrder)
            .ThenBy(d => d.Id)
            .ToListAsync(cancellationToken);
}
