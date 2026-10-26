namespace StartupLab.Models.Entities;

public class UserStartup
{
    public int UserId { get; set; }

    public int StartupId { get; set; }

    public User User { get; set; } = null!;

    public Startup Startup { get; set; } = null!;
}
