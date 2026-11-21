namespace StartupLab.Services.Account;

/// <summary>
/// Whether register or login worked, and the messages to show when it did not.
/// </summary>
public sealed record AccountResult(bool Succeeded, IReadOnlyList<string> Errors)
{
    /// <summary>
    /// A successful register or login.
    /// </summary>
    public static AccountResult Success() => new(true, []);

    /// <summary>
    /// A failed register or login with messages safe to show on the page.
    /// </summary>
    public static AccountResult Failure(params string[] errors) => new(false, errors);
}
