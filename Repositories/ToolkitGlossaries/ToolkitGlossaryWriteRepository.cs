using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitGlossaries.Interfaces;

namespace StartupLab.Repositories.ToolkitGlossaries;

/// <summary>
/// EF Core implementation of <see cref="IToolkitGlossaryWriteRepository"/>.
/// </summary>
public class ToolkitGlossaryWriteRepository(ApplicationDbContext context) : IToolkitGlossaryWriteRepository
{
    public async Task<ToolkitGlossary> CreateAsync(string term, string definition, int displayOrder, CancellationToken cancellationToken)
    {
        var entry = new ToolkitGlossary
        {
            Term = term,
            Definition = definition,
            DisplayOrder = displayOrder,
            DateCreated = DateTime.UtcNow
        };

        context.ToolkitGlossaries.Add(entry);
        await context.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<bool> UpdateAsync(int toolkitGlossaryId, string term, string definition, int displayOrder, CancellationToken cancellationToken) =>
        await context.ToolkitGlossaries
            .Where(g => g.Id == toolkitGlossaryId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(g => g.Term, term)
                .SetProperty(g => g.Definition, definition)
                .SetProperty(g => g.DisplayOrder, displayOrder)
                .SetProperty(g => g.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> DeleteAsync(int toolkitGlossaryId, CancellationToken cancellationToken) =>
        await context.ToolkitGlossaries
            .Where(g => g.Id == toolkitGlossaryId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
}
