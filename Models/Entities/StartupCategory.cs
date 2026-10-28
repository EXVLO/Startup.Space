namespace StartupLab.Models.Entities;

/// <summary>
/// Links a startup to a category. A startup can have many categories.
/// </summary>
public class StartupCategory
{
    public int StartupId { get; set; }

    public int CategoryId { get; set; }

    public Startup Startup { get; set; } = null!;

    public Category Category { get; set; } = null!;
}
