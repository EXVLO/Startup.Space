using Microsoft.AspNetCore.Mvc;
using StartupLab.Models.ViewModels.Account;
using StartupLab.Services.Account.Interfaces;

namespace StartupLab.Controllers.Account;

/// <summary>
/// Register, login, and logout. Routes: /Register, /Login, /Logout.
/// </summary>
public class AccountController(IAccountService accountService) : Controller
{
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return Redirect("/Profile");
        }

        return View(new LoginViewModel());
    }

    [HttpPost("/Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        var result = await accountService.LoginAsync(model.Email ?? string.Empty, model.Password ?? string.Empty, cancellationToken);
        if (!result.Succeeded)
        {
            model.Password = string.Empty;
            model.Errors = result.Errors;
            return View(model);
        }

        return Redirect("/Profile");
    }

    [HttpGet("/Register")]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return Redirect("/Profile");
        }

        return View(new RegisterViewModel());
    }

    [HttpPost("/Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
    {
        if (model.Password != model.RepeatPassword)
        {
            model.Password = string.Empty;
            model.RepeatPassword = string.Empty;
            model.Errors = ["The passwords do not match."];
            return View(model);
        }

        var result = await accountService.RegisterAsync(
            model.Name ?? string.Empty,
            model.Surname ?? string.Empty,
            model.Email ?? string.Empty,
            model.Password ?? string.Empty,
            cancellationToken);
        if (!result.Succeeded)
        {
            model.Password = string.Empty;
            model.RepeatPassword = string.Empty;
            model.Errors = result.Errors;
            return View(model);
        }

        return Redirect("/Profile");
    }

    [HttpPost("/Logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await accountService.LogoutAsync(cancellationToken);
        return Redirect("/");
    }
}
