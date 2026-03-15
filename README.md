# KoreForge.Time

Deterministic and system-backed clock abstractions for KoreForge applications.

## Overview

`KoreForge.Time` provides a thin, testable time abstraction layer:

| Type | Description |
|------|-------------|
| `ISystemClock` | Contract for wall-clock and stopwatch time |
| `LocalSystemClock` | Returns local `DateTimeOffset.Now` |
| `UtcSystemClock` | Returns UTC `DateTimeOffset.UtcNow` |
| `VirtualSystemClock` | Manually controlled clock for deterministic tests |

## Installation

```bash
dotnet add package KoreForge.Time
```

## Quick Start

```csharp
// Production
ISystemClock clock = new UtcSystemClock();
var now = clock.UtcNow;

// Tests
var virtual = new VirtualSystemClock(DateTimeOffset.Parse("2024-01-01T00:00:00Z"));
var now = virtual.UtcNow;  // returns exactly 2024-01-01
virtual.Advance(TimeSpan.FromHours(1));
```

## License

MIT
