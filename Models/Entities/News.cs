namespace StartupLab.Models.Entities;

public class News
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public bool IsPrimary { get; set; }

    public ICollection<NewsPhoto> Photos { get; set; } = [];

    public ICollection<NewsVideo> Videos { get; set; } = [];

    public ICollection<NewsTag> NewsTags { get; set; } = [];
}
