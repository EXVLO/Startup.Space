namespace StartupLab.Models.Entities;

/// <summary>
/// A photo in a news item.
/// </summary>
public class NewsPhoto
{
    public int Id { get; set; }

    public int NewsId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Key of the stored file. Its format is not decided yet.
    /// </summary>
    public string AddressKey { get; set; } = string.Empty;

    /// <summary>
    /// Position in the list. Lower numbers come first.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// True for the news item's main photo.
    /// </summary>
    public bool IsPrimary { get; set; }

    public DateTime DateCreated { get; set; }

    public News News { get; set; } = null!;
}
