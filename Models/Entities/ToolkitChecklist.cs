namespace StartupLab.Models.Entities;

public class ToolkitChecklist
{
    public int Id { get; set; }

    public string Label { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<UserChecklist> UserChecklists { get; set; } = [];
}
