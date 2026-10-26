namespace StartupLab.Models.Entities;

public class UserContact
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ContactInfo { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public User User { get; set; } = null!;
}
