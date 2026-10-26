namespace StartupLab.Models.Entities;

public class UserChecklist
{
    public int UserId { get; set; }

    public int ToolkitChecklistId { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime DateCompleted { get; set; }

    public User User { get; set; } = null!;

    public ToolkitChecklist ToolkitChecklist { get; set; } = null!;
}
