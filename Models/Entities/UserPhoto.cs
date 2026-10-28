namespace StartupLab.Models.Entities;

/// <summary>
/// The profile photo of a user. A user has at most one.
/// </summary>
public class UserPhoto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// Key of the stored file. Its format is not decided yet.
    /// </summary>
    public string AddressKey { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public User User { get; set; } = null!;
}
