using Microsoft.AspNetCore.Mvc;
using StartupLab.Models.ViewModels.Settings;
using StartupLab.Services.Account.Interfaces;
using StartupLab.Services.Users.Interfaces;

namespace StartupLab.Controllers.Users;

/// <summary>
/// The signed-in user's account settings. Route: /Settings.
/// Password and email changes are still open (Q-PR-3) and stay with account login.
/// </summary>
[Route("Settings")]
public class SettingsController(IUserService userService, IAccountService accountService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        var user = await userService.GetProfileAsync(userId, cancellationToken);
        if (user is null)
        {
            await accountService.LogoutAsync(cancellationToken);
            return Redirect("/Login");
        }

        return View(new SettingsViewModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive
        });
    }

    [HttpPost("Deactivate")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (!await userService.DeactivateAsync(userId, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (await userService.DeleteAsync(userId, cancellationToken) is null)
        {
            return NotFound();
        }

        await accountService.LogoutAsync(cancellationToken);
        return Redirect("/");
    }
}
