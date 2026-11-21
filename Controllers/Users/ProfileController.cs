using Microsoft.AspNetCore.Mvc;
using StartupLab.Models.ViewModels.Profile;
using StartupLab.Services.Account.Interfaces;
using StartupLab.Services.Users.Interfaces;

namespace StartupLab.Controllers.Users;

/// <summary>
/// The signed-in user's profile: name, photo, contacts and experience. Route: /Profile.
/// </summary>
[Route("Profile")]
public class ProfileController(IUserService userService, IAccountService accountService) : Controller
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

        return View(new ProfileViewModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Description = user.Description,
            Email = user.Email,
            PhotoAddressKey = user.Photo?.AddressKey,
            Contacts = user.Contacts
                .Select(c => new ProfileContactViewModel { ContactId = c.Id, ContactInfo = c.ContactInfo })
                .ToList(),
            Experiences = user.Experiences
                .Select(e => new ProfileExperienceViewModel { ExperienceId = e.Id, Title = e.Title, Description = e.Description })
                .ToList()
        });
    }

    [HttpPost("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(string name, string surname, string? description, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
        {
            TempData["Error"] = "Name and surname are required.";
            return RedirectToAction(nameof(Index));
        }

        if (!await userService.UpdateProfileAsync(userId, name, surname, description, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Photo")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Photo(string addressKey, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (string.IsNullOrWhiteSpace(addressKey))
        {
            TempData["Error"] = "A photo address key is required.";
            return RedirectToAction(nameof(Index));
        }

        var change = await userService.SetPhotoAsync(userId, addressKey, cancellationToken);
        if (!change.Saved)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("DeletePhoto")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePhoto(CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        await userService.DeletePhotoAsync(userId, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("AddContact")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddContact(string contactInfo, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (string.IsNullOrWhiteSpace(contactInfo))
        {
            TempData["Error"] = "Contact info is required.";
            return RedirectToAction(nameof(Index));
        }

        if (await userService.AddContactAsync(userId, contactInfo, cancellationToken) is null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("UpdateContact")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateContact(int contactId, string contactInfo, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (string.IsNullOrWhiteSpace(contactInfo))
        {
            TempData["Error"] = "Contact info is required.";
            return RedirectToAction(nameof(Index));
        }

        if (!await userService.UpdateContactAsync(userId, contactId, contactInfo, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("DeleteContact")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteContact(int contactId, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (!await userService.DeleteContactAsync(userId, contactId, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("AddExperience")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddExperience(string? title, string? description, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (await userService.AddExperienceAsync(userId, title, description, cancellationToken) is null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("UpdateExperience")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateExperience(int experienceId, string? title, string? description, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (!await userService.UpdateExperienceAsync(userId, experienceId, title, description, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("DeleteExperience")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteExperience(int experienceId, CancellationToken cancellationToken)
    {
        if (CurrentUser.Id(User) is not int userId)
        {
            return Redirect("/Login");
        }

        if (!await userService.DeleteExperienceAsync(userId, experienceId, cancellationToken))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
