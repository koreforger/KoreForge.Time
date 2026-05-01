using System;
using System.Diagnostics;

namespace KoreForge.Time.Tests;

public class VirtualSystemClockTests
{
    [Fact]
    public void Constructor_Uses_Provided_Start_Time()
    {
        var start = new DateTimeOffset(2024, 01, 02, 03, 04, 05, TimeSpan.Zero);
        var clock = new VirtualSystemClock(start);

        Assert.Equal(start, clock.Now);
        Assert.Equal(start, clock.UtcNow);
        Assert.Equal(0, clock.TimestampTicks);
    }

    [Fact]
    public void Advance_Moves_Time_Forward_And_Increments_Timestamp()
    {
        var start = new DateTimeOffset(2024, 05, 06, 07, 08, 09, TimeSpan.FromHours(1));
        var clock = new VirtualSystemClock(start);
        var delta = TimeSpan.FromSeconds(2.5);

        clock.Advance(delta);

        Assert.Equal(start + delta, clock.Now);
        Assert.Equal((long)(Stopwatch.Frequency * delta.TotalSeconds), clock.TimestampTicks);
    }

    [Fact]
    public void Set_Overrides_Current_Time()
    {
        var clock = new VirtualSystemClock(new DateTimeOffset(2024, 01, 01, 0, 0, 0, TimeSpan.Zero));
        var newTime = clock.Now.AddHours(3).AddMinutes(15);

        clock.Set(newTime);

        Assert.Equal(newTime, clock.Now);
        Assert.Equal(newTime.ToUniversalTime(), clock.UtcNow);
    }

    [Fact]
    public void Advance_With_Negative_TimeSpan_Throws()
    {
        var clock = new VirtualSystemClock();

        Assert.Throws<ArgumentOutOfRangeException>(() => clock.Advance(TimeSpan.FromMilliseconds(-1)));
    }
}
