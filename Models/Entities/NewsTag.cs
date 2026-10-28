namespace StartupLab.Models.Entities;

/// <summary>
/// Links a news item to a tag. A news item can have many tags.
/// </summary>
public class NewsTag
{
    public int NewsId { get; set; }

    public int TagId { get; set; }

    public News News { get; set; } = null!;

    public Tag Tag { get; set; } = null!;
}
