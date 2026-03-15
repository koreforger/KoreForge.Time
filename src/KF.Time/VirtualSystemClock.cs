using System;
using System.Diagnostics;
using System.Threading;

namespace KF.Time;

/// <summary>
/// Deterministic clock intended for tests that need to control the perceived time.
/// </summary>
public sealed class VirtualSystemClock : ISystemClock
{
    private DateTimeOffset _now;
    private long _timestampTicks;

    /// <summary>
    /// Initializes the clock at the supplied starting instant or uses <see cref="DateTimeOffset.Now"/> when omitted.
    /// </summary>
    /// <param name="start">Optional starting instant for the virtual clock.</param>
    public VirtualSystemClock(DateTimeOffset? start = null)
    {
        _now = start ?? DateTimeOffset.Now;
        _timestampTicks = 0;
    }

    /// <inheritdoc />
    public DateTimeOffset Now => _now;

    /// <inheritdoc />
    public DateTimeOffset UtcNow => _now.ToUniversalTime();

    /// <inheritdoc />
    public long TimestampTicks => Volatile.Read(ref _timestampTicks);

    /// <summary>
    /// Advances the perceived time by the requested amount without ever moving backwards.
    /// </summary>
    /// <param name="amount">A positive duration describing how far to move.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is negative.</exception>
    public void Advance(TimeSpan amount)
    {
        if (amount < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Cannot rewind virtual clock");
        }

        _now += amount;
        var increment = (long)(_stopwatchFrequency * amount.TotalSeconds);
        Interlocked.Add(ref _timestampTicks, increment);
    }

    /// <summary>
    /// Forces the clock to report a specific timestamp; timestamp ticks remain unchanged.
    /// </summary>
    /// <param name="timestamp">The new instant that should be reported.</param>
    public void Set(DateTimeOffset timestamp)
    {
        _now = timestamp;
    }

    private static readonly double _stopwatchFrequency = Stopwatch.Frequency;
}
