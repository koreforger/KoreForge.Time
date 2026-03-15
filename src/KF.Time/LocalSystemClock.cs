using System;
using System.Diagnostics;

namespace KF.Time;

/// <summary>
/// Local time implementation that surfaces local timestamps while still exposing UTC when required.
/// </summary>
public sealed class LocalSystemClock : ISystemClock
{
    /// <summary>
    /// Gets the shared singleton instance for callers that do not need their own clock.
    /// </summary>
    public static LocalSystemClock Instance { get; } = new();

    private LocalSystemClock()
    {
    }

    /// <inheritdoc />
    public DateTimeOffset Now => DateTimeOffset.Now;

    /// <inheritdoc />
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public long TimestampTicks => Stopwatch.GetTimestamp();
}
