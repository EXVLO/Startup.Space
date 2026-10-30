namespace StartupLab.Models.Entities;

/// <summary>
/// A tutorial video in the Founders Toolkit.
/// </summary>
public class ToolkitVideo
{
    public int Id { get; set; }

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
}
