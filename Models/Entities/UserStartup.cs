namespace StartupLab.Models.Entities;

/// <summary>
/// Links a user to a startup they founded. A startup can have several founders.
/// </summary>
public class UserStartup
{
    public int UserId { get; set; }

    public int StartupId { get; set; }

    public User User { get; set; } = null!;

    public Startup Startup { get; set; } = null!;
}
