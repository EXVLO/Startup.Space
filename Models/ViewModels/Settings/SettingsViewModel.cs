namespace StartupLab.Models.ViewModels.Settings;

/// <summary>
/// The signed-in user's settings page.
/// </summary>
public class SettingsViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string? Email { get; set; }

    public bool IsActive { get; set; }
}
