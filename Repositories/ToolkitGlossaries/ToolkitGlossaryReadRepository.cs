using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitGlossaries.Interfaces;

namespace StartupLab.Repositories.ToolkitGlossaries;

/// <summary>
/// EF Core implementation of <see cref="IToolkitGlossaryReadRepository"/>.
/// </summary>
public class ToolkitGlossaryReadRepository(ApplicationDbContext context) : IToolkitGlossaryReadRepository
{
    public async Task<ToolkitGlossary?> GetByIdAsync(int toolkitGlossaryId, CancellationToken cancellationToken) =>
        await context.ToolkitGlossaries
            .AsNoTracking()
            .SingleOrDefaultAsync(g => g.Id == toolkitGlossaryId, cancellationToken);

    public async Task<IReadOnlyList<ToolkitGlossary>> GetEntriesAsync(int max, CancellationToken cancellationToken) =>
        await context.ToolkitGlossaries
            .AsNoTracking()
            .OrderBy(g => g.DisplayOrder)
            .ThenBy(g => g.Id)
            .Take(max)
            .ToListAsync(cancellationToken);
}
