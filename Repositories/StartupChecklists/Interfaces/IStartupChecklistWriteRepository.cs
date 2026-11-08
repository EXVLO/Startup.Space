using StartupLab.Models.Entities;

namespace StartupLab.Repositories.StartupChecklists.Interfaces;

/// <summary>
/// Changes the startup checklist and marks each startup's progress on it.
/// </summary>
public interface IStartupChecklistWriteRepository
{
    /// <summary>
    /// Marks a startup checklist item as done or not done for the startup. Returns false if the startup or the item doesn't exist.
    /// </summary>
    Task<bool> SetProgressAsync(int startupId, int startupChecklistId, bool isCompleted, CancellationToken cancellationToken);

    /// <summary>
    /// Adds an item to the startup checklist and returns it with its new id. Every startup sees the new item as not done.
    /// </summary>
    Task<StartupChecklist> AddItemAsync(string label, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a startup checklist item. Returns false if the item doesn't exist.
    /// </summary>
    Task<bool> UpdateItemAsync(int startupChecklistId, string label, int displayOrder, bool isActive, CancellationToken cancellationToken);
}
