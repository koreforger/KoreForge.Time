namespace KoreForge.Time;

/// <summary>
/// Provides convenient access to the default system clock (local time).
/// </summary>
public static class SystemClock
{
    /// <summary>
    /// Default clock used when callers do not explicitly supply one.
    /// Defaults to <see cref="LocalSystemClock"/>.
    /// </summary>
    public static ISystemClock Instance { get; } = LocalSystemClock.Instance;
}
