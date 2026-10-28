namespace StartupLab.Models.Entities;

/// <summary>
/// One item of the founder checklist in the Founders Toolkit. Progress is stored in <see cref="UserChecklist"/>.
/// </summary>
public class ToolkitChecklist
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

    public ICollection<UserChecklist> UserChecklists { get; set; } = [];
}
