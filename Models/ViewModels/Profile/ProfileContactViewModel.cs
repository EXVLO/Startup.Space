namespace StartupLab.Models.ViewModels.Profile;

/// <summary>
/// One contact shown on the profile page.
/// </summary>
public class ProfileContactViewModel
{
    public int ContactId { get; set; }

    public string ContactInfo { get; set; } = string.Empty;
}
