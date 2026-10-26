namespace StartupLab.Models.Entities;

public class StartupCategory
{
    public int StartupId { get; set; }

    public int CategoryId { get; set; }

    public Startup Startup { get; set; } = null!;

    public Category Category { get; set; } = null!;
}
