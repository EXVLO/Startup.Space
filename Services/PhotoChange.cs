namespace StartupLab.Services;

/// <summary>
/// Result of setting a user's photo.
/// </summary>
public sealed record PhotoChange(bool Saved, string? PreviousAddressKey);
