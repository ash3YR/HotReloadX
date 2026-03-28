using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotReloadX.Core.Services;

public class PipelineManager
{
    private readonly BuildManager _build;
    private readonly ProcessManager _process;

    private CancellationTokenSource? _cts;

    public PipelineManager(BuildManager build, ProcessManager process)
    {
        _build = build;
        _process = process;
    }

    public void Trigger(string buildCmd, string runCmd)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        var token = _cts.Token;

        Task.Run(async () =>
        {
            try
            {
                _process.Stop();

                await Task.Delay(500, token);

                if (!_build.Execute(buildCmd)) return;

                if (token.IsCancellationRequested) return;

                _process.Start(runCmd);
            }
            catch (OperationCanceledException)
            {
                ConsoleHelper.Warning("⚠️ Build cancelled");
            }
        }, token);
    }
}