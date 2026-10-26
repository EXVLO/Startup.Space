namespace StartupLab.Models.Entities;

public class ToolkitVideo
{
    public int Id { get; set; }

    public string AddressKey { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }
}
