using Microsoft.AspNetCore.Identity;

namespace StartupLab.Models.Entities;

/// <summary>
/// A person with an account. Every user is a founder, and an admin can also make them a mentor.
/// Login data such as email and password hash comes from <see cref="IdentityUser{TKey}"/>.
/// </summary>
public class User : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// True if an admin gave this user the mentor role.
    /// </summary>
    public bool IsMentor { get; set; } = false;

    /// <summary>
    /// True if this user can manage other users, for example give the mentor role.
    /// </summary>
    public bool IsAdmin { get; set; } = false;

    /// <summary>
    /// False when the account is deactivated.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The profile photo, or null if the user has none.
    /// </summary>
    public UserPhoto? Photo { get; set; }

    public ICollection<UserContact> Contacts { get; set; } = [];

    public ICollection<UserExperience> Experiences { get; set; } = [];

    public ICollection<UserStartup> UserStartups { get; set; } = [];

    public ICollection<UserChecklist> UserChecklists { get; set; } = [];
}
