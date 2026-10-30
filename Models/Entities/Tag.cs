namespace StartupLab.Models.Entities;

/// <summary>
/// A tag used to group and filter news.
/// </summary>
public class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    /// <summary>
    /// False when the tag is no longer used.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public ICollection<NewsTag> NewsTags { get; set; } = [];
}
