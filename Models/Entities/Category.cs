namespace StartupLab.Models.Entities;

/// <summary>
/// A category used to group and filter startups.
/// </summary>
public class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    /// <summary>
    /// False when the category is no longer used.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public ICollection<StartupCategory> StartupCategories { get; set; } = [];
}
