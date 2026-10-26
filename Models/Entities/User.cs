using Microsoft.AspNetCore.Identity;

namespace StartupLab.Models.Entities;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public string? Description { get; set; }

    public bool IsMentor { get; set; } = false;

    public bool IsAdmin { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public UserPhoto? Photo { get; set; }

    public ICollection<UserContact> Contacts { get; set; } = [];

    public ICollection<UserExperience> Experiences { get; set; } = [];

    public ICollection<UserStartup> UserStartups { get; set; } = [];

    public ICollection<UserChecklist> UserChecklists { get; set; } = [];
}
