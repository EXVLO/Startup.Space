namespace StartupLab.Models.Entities;

/// <summary>
/// A document on a startup's page, for example a pitch deck.
/// </summary>
public class StartupDocument
{
    public int Id { get; set; }

    public int StartupId { get; set; }

    /// <summary>
    /// Key of the stored file. Its format is not decided yet.
    /// </summary>
    public string AddressKey { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Position in the list. Lower numbers come first.
    /// </summary>
    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }

    public Startup Startup { get; set; } = null!;
}
