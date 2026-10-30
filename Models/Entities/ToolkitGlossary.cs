namespace StartupLab.Models.Entities;

/// <summary>
/// A term and its definition in the Founders Toolkit glossary.
/// </summary>
public class ToolkitGlossary
{
    public int Id { get; set; }

    /// <summary>
    /// Key of a stored file. Why the glossary needs one is still an open question.
    /// </summary>
    public string AddressKey { get; set; } = string.Empty;

    public string Term { get; set; } = string.Empty;

    public string Definition { get; set; } = string.Empty;

    /// <summary>
    /// Position in the list. Lower numbers come first.
    /// </summary>
    public int DisplayOrder { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
