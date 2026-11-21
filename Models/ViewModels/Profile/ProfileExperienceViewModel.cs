namespace StartupLab.Models.ViewModels.Profile;

/// <summary>
/// One experience entry shown on the profile page.
/// </summary>
public class ProfileExperienceViewModel
{
    public int ExperienceId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}
