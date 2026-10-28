namespace StartupLab.Models.Entities;

/// <summary>
/// One experience entry on a user's profile, for example a past job. A user can have many.
/// </summary>
public class UserExperience
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public User User { get; set; } = null!;
}
