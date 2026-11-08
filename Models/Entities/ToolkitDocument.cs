namespace StartupLab.Models.Entities;

/// <summary>
/// A document in the Founders Toolkit.
/// </summary>
public class ToolkitDocument
{
    public int Id { get; set; }

    /// <summary>
    /// Link to the document. Toolkit documents are external links, not stored files.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Position in the list. Lower numbers come first.
    /// </summary>
    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }
}
