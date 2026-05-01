using System;

namespace KoreForge.Time;

/// <summary>
/// Represents the single source of truth for wall-clock and stopwatch-based time measurements.
/// </summary>
public interface ISystemClock
{
    /// <summary>
    /// Current local time including offset information.
    /// </summary>
    DateTimeOffset Now { get; }

    /// <summary>
    /// Current UTC time.
    /// </summary>
    DateTimeOffset UtcNow { get; }

    /// <summary>
    /// Current high-resolution timestamp ticks sourced from <see cref="System.Diagnostics.Stopwatch"/>.
    /// </summary>
    long TimestampTicks { get; }
}
