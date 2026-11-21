namespace StartupLab.Models.ViewModels.Account;

/// <summary>
/// The login form.
/// </summary>
public class LoginViewModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public IReadOnlyList<string> Errors { get; set; } = [];
}
