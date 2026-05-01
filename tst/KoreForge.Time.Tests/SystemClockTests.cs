using System;

namespace KoreForge.Time.Tests;

public class SystemClockTests
{
    [Fact]
    public void Instance_Defaults_To_LocalClock()
    {
        var first = SystemClock.Instance;
        var second = SystemClock.Instance;

        Assert.Same(LocalSystemClock.Instance, first);
        Assert.Same(first, second);
    }

    [Fact]
    public void LocalSystemClock_Mirrors_Current_System_Time()
    {
        var clock = LocalSystemClock.Instance;

        var before = DateTimeOffset.Now;
        var now = clock.Now;
        var after = DateTimeOffset.Now;

        Assert.InRange(now, before - TimeSpan.FromSeconds(1), after + TimeSpan.FromSeconds(1));

        var utcBefore = DateTimeOffset.UtcNow;
        var utc = clock.UtcNow;
        var utcAfter = DateTimeOffset.UtcNow;

        Assert.InRange(utc, utcBefore - TimeSpan.FromSeconds(1), utcAfter + TimeSpan.FromSeconds(1));

        var firstTicks = clock.TimestampTicks;
        var secondTicks = clock.TimestampTicks;
        Assert.True(secondTicks >= firstTicks, "Timestamp ticks should be monotonic");
    }

    [Fact]
    public void UtcSystemClock_Returns_Utc_Time_With_Zero_Offset()
    {
        var clock = UtcSystemClock.Instance;
        var now = clock.Now;
        var utcNow = clock.UtcNow;

        Assert.Equal(TimeSpan.Zero, now.Offset);
        Assert.True((now - utcNow).Duration() <= TimeSpan.FromMilliseconds(5), "UTC clock should provide consistent timestamps");

        var before = DateTimeOffset.UtcNow - TimeSpan.FromSeconds(1);
        var after = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(1);

        Assert.InRange(now, before, after);

        var tick1 = clock.TimestampTicks;
        var tick2 = clock.TimestampTicks;
        Assert.True(tick2 >= tick1, "Timestamp ticks should be monotonic");
    }
}
