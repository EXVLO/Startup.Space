namespace StartupLab.Models.Entities;

public class ToolkitGlossary
{
    public int Id { get; set; }

    public string AddressKey { get; set; } = string.Empty;

    public string Term { get; set; } = string.Empty;

    public string Definition { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
