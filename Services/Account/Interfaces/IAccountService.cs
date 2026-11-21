namespace StartupLab.Services.Account.Interfaces;

/// <summary>
/// Registers a user, signs them in, and signs them out. Passwords stay in ASP.NET Core Identity.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Creates an account and signs it in. Fails when a field is blank, the email is already used, or the password is too weak.
    /// </summary>
    Task<AccountResult> RegisterAsync(string name, string surname, string email, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Signs in with email and password. Fails when they do not match or the account is inactive.
    /// </summary>
    Task<AccountResult> LoginAsync(string email, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Signs the current user out.
    /// </summary>
    Task LogoutAsync(CancellationToken cancellationToken);
}
