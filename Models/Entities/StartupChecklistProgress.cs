namespace StartupLab.Models.Entities;

/// <summary>
/// A startup's progress on one item of the startup checklist. A missing row means the item is not done.
/// </summary>
public class StartupChecklistProgress
{
    public int StartupId { get; set; }

    public int StartupChecklistId { get; set; }

    public bool IsCompleted { get; set; }

    /// <summary>
    /// When the item was marked done, or null while it is not done.
    /// </summary>
    public DateTime? DateCompleted { get; set; }

    public Startup Startup { get; set; } = null!;

    public StartupChecklist StartupChecklist { get; set; } = null!;
}
