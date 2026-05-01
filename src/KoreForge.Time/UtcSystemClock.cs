using System;
using System.Diagnostics;

namespace KoreForge.Time;

/// <summary>
/// UTC-only implementation that treats local and UTC time as equivalent for deterministic output.
/// </summary>
public sealed class UtcSystemClock : ISystemClock
{
    /// <summary>
    /// Gets the shared singleton instance for UTC-only time access.
    /// </summary>
    public static UtcSystemClock Instance { get; } = new();

    private UtcSystemClock()
    {
    }

    /// <inheritdoc />
    public DateTimeOffset Now => DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public long TimestampTicks => Stopwatch.GetTimestamp();
}
