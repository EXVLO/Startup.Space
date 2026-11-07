namespace StartupLab.Models.Entities;

/// <summary>
/// One item of the checklist every startup follows. Each startup's progress is stored in <see cref="StartupChecklistProgress"/>.
/// </summary>
public class StartupChecklist
{
    public int Id { get; set; }

    /// <summary>
    /// The text shown for the item.
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Position in the list. Lower numbers come first.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// False when the item is no longer shown.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public ICollection<StartupChecklistProgress> Progress { get; set; } = [];
}
