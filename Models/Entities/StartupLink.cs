namespace StartupLab.Models.Entities;

/// <summary>
/// A link on a startup's page, for example its website.
/// </summary>
public class StartupLink
{
    public int Id { get; set; }

    public int StartupId { get; set; }

    /// <summary>
    /// The URL.
    /// </summary>
    public string Link { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Position in the list. Lower numbers come first.
    /// </summary>
    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }

    public Startup Startup { get; set; } = null!;
}
