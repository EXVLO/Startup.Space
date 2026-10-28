namespace StartupLab.Models.Entities;

/// <summary>
/// A way to contact a user, for example an email address or a phone number. A user can have many.
/// </summary>
public class UserContact
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ContactInfo { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public User User { get; set; } = null!;
}
