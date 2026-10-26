namespace StartupLab.Models.Entities;

public class UserPhoto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string AddressKey { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public User User { get; set; } = null!;
}
