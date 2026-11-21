using Microsoft.AspNetCore.Mvc;
using StartupLab.Models.ViewModels.Settings;
using StartupLab.Services.Interfaces;

namespace StartupLab.Controllers;

/// <summary>
/// The signed-in user's account settings. Route: /Settings.
/// Password and email changes are still open (Q-PR-3) and stay with account login.
/// </summary>
[Route("Settings")]
public class SettingsController(IUserService userService) : Controller
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
            return NotFound();
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

        return Redirect("/");
    }
}
