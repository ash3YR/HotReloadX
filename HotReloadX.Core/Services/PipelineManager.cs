using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotReloadX.Core.Services;

public class PipelineManager
{
    private readonly BuildManager _build;
    private readonly ProcessManager _process;

    private CancellationTokenSource? _cts;
    private readonly object _lock = new();

    public PipelineManager(BuildManager b, ProcessManager p)
    {
        _build = b;
        _process = p;
    }

    public void Trigger(string buildCmd, string runCmd)
    {
        lock (_lock)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            var token = _cts.Token;

            Task.Run(async () =>
            {
                try
                {
                    Console.WriteLine("\n🔥 Change detected → Reloading...\n");

                    _process.Stop();

                    await Task.Delay(700, token);

                    if (!_build.Execute(buildCmd)) return;

                    if (token.IsCancellationRequested) return;

                    _process.Start(runCmd);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("⚠️ Build cancelled");
                }
            }, token);
        }
    }
}