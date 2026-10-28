namespace StartupLab.Models.Entities;

/// <summary>
/// A user's progress on one item of the founder checklist.
/// </summary>
public class UserChecklist
{
    public int UserId { get; set; }

    public int ToolkitChecklistId { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime DateCompleted { get; set; }

    public User User { get; set; } = null!;

    public ToolkitChecklist ToolkitChecklist { get; set; } = null!;
}
