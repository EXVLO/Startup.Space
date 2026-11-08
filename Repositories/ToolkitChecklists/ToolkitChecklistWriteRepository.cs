using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.ToolkitChecklists.Interfaces;

namespace StartupLab.Repositories.ToolkitChecklists;

/// <summary>
/// EF Core implementation of <see cref="IToolkitChecklistWriteRepository"/>.
/// </summary>
public class ToolkitChecklistWriteRepository(ApplicationDbContext context) : IToolkitChecklistWriteRepository
{
    public async Task<bool> SetProgressAsync(int userId, int toolkitChecklistId, bool isCompleted, CancellationToken cancellationToken)
    {
        if (!await context.ToolkitChecklists.AnyAsync(c => c.Id == toolkitChecklistId, cancellationToken))
        {
            return false;
        }

        var progress = await context.ToolkitChecklistProgress
            .SingleOrDefaultAsync(tp => tp.UserId == userId && tp.ToolkitChecklistId == toolkitChecklistId, cancellationToken);

        if (progress is null)
        {
            if (!isCompleted)
            {
                return true;
            }

            progress = new ToolkitChecklistProgress { UserId = userId, ToolkitChecklistId = toolkitChecklistId };
            context.ToolkitChecklistProgress.Add(progress);
        }

        // date_completed is required on this table, so unchecking keeps the last completion date.
        progress.IsCompleted = isCompleted;
        if (isCompleted)
        {
            progress.DateCompleted = DateTime.UtcNow;
        }

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ToolkitChecklist> AddItemAsync(string label, int displayOrder, CancellationToken cancellationToken)
    {
        var item = new ToolkitChecklist
        {
            Label = label,
            DisplayOrder = displayOrder
        };

        context.ToolkitChecklists.Add(item);
        await context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<bool> UpdateItemAsync(int toolkitChecklistId, string label, int displayOrder, bool isActive, CancellationToken cancellationToken) =>
        await context.ToolkitChecklists
            .Where(c => c.Id == toolkitChecklistId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Label, label)
                .SetProperty(c => c.DisplayOrder, displayOrder)
                .SetProperty(c => c.IsActive, isActive),
                cancellationToken) > 0;
}
