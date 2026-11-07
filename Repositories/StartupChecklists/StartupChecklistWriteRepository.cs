using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.StartupChecklists.Interfaces;

namespace StartupLab.Repositories.StartupChecklists;

/// <summary>
/// EF Core implementation of <see cref="IStartupChecklistWriteRepository"/>.
/// </summary>
public class StartupChecklistWriteRepository(ApplicationDbContext context) : IStartupChecklistWriteRepository
{
    public async Task<bool> SetProgressAsync(int startupId, int startupChecklistId, bool isCompleted, CancellationToken cancellationToken)
    {
        if (!await context.Startups.AnyAsync(s => s.Id == startupId, cancellationToken)
            || !await context.StartupChecklists.AnyAsync(c => c.Id == startupChecklistId, cancellationToken))
        {
            return false;
        }

        var progress = await context.StartupChecklistProgress
            .SingleOrDefaultAsync(sp => sp.StartupId == startupId && sp.StartupChecklistId == startupChecklistId, cancellationToken);

        if (progress is null)
        {
            if (!isCompleted)
            {
                return true;
            }

            progress = new StartupChecklistProgress { StartupId = startupId, StartupChecklistId = startupChecklistId };
            context.StartupChecklistProgress.Add(progress);
        }

        progress.IsCompleted = isCompleted;
        progress.DateCompleted = isCompleted ? DateTime.UtcNow : null;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<StartupChecklist> AddItemAsync(string label, int displayOrder, CancellationToken cancellationToken)
    {
        var item = new StartupChecklist
        {
            Label = label,
            DisplayOrder = displayOrder
        };

        context.StartupChecklists.Add(item);
        await context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<bool> UpdateItemAsync(int startupChecklistId, string label, int displayOrder, bool isActive, CancellationToken cancellationToken) =>
        await context.StartupChecklists
            .Where(c => c.Id == startupChecklistId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Label, label)
                .SetProperty(c => c.DisplayOrder, displayOrder)
                .SetProperty(c => c.IsActive, isActive),
                cancellationToken) > 0;
}
