namespace StartupLab.Models.Entities;

public class StartupLink
{
    public int Id { get; set; }

    public int StartupId { get; set; }

    public string Link { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }

    public Startup Startup { get; set; } = null!;
}
