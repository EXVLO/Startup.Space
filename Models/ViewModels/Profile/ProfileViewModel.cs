namespace StartupLab.Models.ViewModels.Profile;

/// <summary>
/// The signed-in user's profile page.
/// </summary>
public class ProfileViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? PhotoAddressKey { get; set; }

    public IReadOnlyList<ProfileContactViewModel> Contacts { get; set; } = [];

    public IReadOnlyList<ProfileExperienceViewModel> Experiences { get; set; } = [];
}
