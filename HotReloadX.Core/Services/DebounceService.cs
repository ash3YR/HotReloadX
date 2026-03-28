using System;
using System.Threading;

namespace HotReloadX.Core.Services;

public class DebounceService
{
    private Timer? _timer;
    private readonly int _delayMs;

    public DebounceService(int delayMs = 500)
    {
        _delayMs = delayMs;
    }

    public void Execute(Action action)
    {
        _timer?.Dispose();

        _timer = new Timer(_ =>
        {
            action();
        }, null, _delayMs, Timeout.Infinite);
    }
}