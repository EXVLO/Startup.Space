namespace StartupLab.Models.ViewModels.Account;

/// <summary>
/// The registration form.
/// </summary>
public class RegisterViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string RepeatPassword { get; set; } = string.Empty;

    public IReadOnlyList<string> Errors { get; set; } = [];
}
