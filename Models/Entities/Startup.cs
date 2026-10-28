namespace StartupLab.Models.Entities;

/// <summary>
/// A startup that its founders publish on the site.
/// </summary>
public class Startup
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    /// <summary>
    /// False when the startup is inactive. Inactive startups are shown in the Archive.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public ICollection<StartupPhoto> Photos { get; set; } = [];

    public ICollection<StartupVideo> Videos { get; set; } = [];

    public ICollection<StartupDocument> Documents { get; set; } = [];

    public ICollection<StartupLink> Links { get; set; } = [];

    public ICollection<StartupCategory> StartupCategories { get; set; } = [];

    public ICollection<UserStartup> UserStartups { get; set; } = [];
}
