using Microsoft.AspNetCore.Identity;
using StartupLab.Models.Entities;
using StartupLab.Services.Account.Interfaces;

namespace StartupLab.Services.Account;

/// <summary>
/// Implementation of <see cref="IAccountService"/>.
/// </summary>
public class AccountService(UserManager<User> userManager, SignInManager<User> signInManager) : IAccountService
{
    public async Task<AccountResult> RegisterAsync(string name, string surname, string email, string password, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        name = name.Trim();
        surname = surname.Trim();
        email = email.Trim();
        if (name.Length == 0 || surname.Length == 0 || email.Length == 0 || string.IsNullOrEmpty(password))
        {
            return AccountResult.Failure("Name, surname, email, and password are required.");
        }

        var user = new User
        {
            UserName = email,
            Email = email,
            Name = name,
            Surname = surname,
            DateCreated = DateTime.UtcNow
        };

        var created = await userManager.CreateAsync(user, password);
        if (!created.Succeeded)
        {
            return new AccountResult(false, created.Errors.Select(error => error.Description).ToList());
        }

        await signInManager.SignInAsync(user, isPersistent: false);
        return AccountResult.Success();
    }

    public async Task<AccountResult> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        email = email.Trim();
        if (email.Length == 0 || string.IsNullOrEmpty(password))
        {
            return AccountResult.Failure("Email and password are required.");
        }

        var user = await userManager.FindByEmailAsync(email);
        if (user is null || !user.IsActive)
        {
            return AccountResult.Failure("Email or password is wrong.");
        }

        var signedIn = await signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
        if (!signedIn.Succeeded)
        {
            return AccountResult.Failure("Email or password is wrong.");
        }

        return AccountResult.Success();
    }

    public async Task LogoutAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await signInManager.SignOutAsync();
    }
}
