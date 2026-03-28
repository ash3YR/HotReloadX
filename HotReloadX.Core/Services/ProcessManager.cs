using System;
using System.Diagnostics;
using System.Threading;

namespace HotReloadX.Core.Services;

public class ProcessManager
{
    private Process? _process;

    public void Start(string command)
    {
        Stop();

        ConsoleHelper.Success("🚀 Starting server...\n");

        var parts = Split(command);

        var psi = new ProcessStartInfo
        {
            FileName = parts.Item1,
            Arguments = parts.Item2,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        _process = new Process { StartInfo = psi };

        _process.OutputDataReceived += (_, e) =>
        {
            if (e.Data != null) Console.WriteLine(e.Data);
        };

        _process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data != null) ConsoleHelper.Error(e.Data);
        };

        _process.Start();
        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();
    }

    public void Stop()
    {
        try
        {
            if (_process != null && !_process.HasExited)
            {
                ConsoleHelper.Warning("🛑 Stopping previous server...");

                var psi = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/PID {_process.Id} /T /F",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using var proc = Process.Start(psi);
                proc.WaitForExit();

                Thread.Sleep(500);
            }
        }
        catch { }
    }

    private (string, string) Split(string cmd)
    {
        var i = cmd.IndexOf(' ');
        return i == -1 ? (cmd, "") : (cmd[..i], cmd[(i + 1)..]);
    }
}