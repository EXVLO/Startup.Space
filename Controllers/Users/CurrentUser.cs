using System.Security.Claims;

namespace StartupLab.Controllers.Users;

/// <summary>
/// Reads the signed-in user's id from the authentication cookie.
/// </summary>
static class CurrentUser
{
    /// <summary>
    /// Gets the signed-in user's id, or null if nobody is signed in.
    /// </summary>
    public static int? Id(ClaimsPrincipal principal)
    {
        var value = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var userId) ? userId : null;
    }
}
