namespace StartupLab.Models.Entities;

public class NewsPhoto
{
    public int Id { get; set; }

    public int NewsId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string AddressKey { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }

    public DateTime DateCreated { get; set; }

    public News News { get; set; } = null!;
}
