namespace StartupLab.Models.Entities;

public class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<NewsTag> NewsTags { get; set; } = [];
}
