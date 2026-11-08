using StartupLab.Models.Entities;

namespace StartupLab.Repositories.ToolkitChecklists.Interfaces;

/// <summary>
/// Changes the toolkit checklist and marks each user's progress on it.
/// </summary>
public interface IToolkitChecklistWriteRepository
{
    /// <summary>
    /// Marks a toolkit checklist item as done or not done for the user. Returns false if the item doesn't exist.
    /// </summary>
    Task<bool> SetProgressAsync(int userId, int toolkitChecklistId, bool isCompleted, CancellationToken cancellationToken);

    /// <summary>
    /// Adds an item to the toolkit checklist and returns it with its new id.
    /// </summary>
    Task<ToolkitChecklist> AddItemAsync(string label, int displayOrder, CancellationToken cancellationToken);

    /// <summary>
    /// Changes a toolkit checklist item. Returns false if the item doesn't exist.
    /// </summary>
    Task<bool> UpdateItemAsync(int toolkitChecklistId, string label, int displayOrder, bool isActive, CancellationToken cancellationToken);
}
