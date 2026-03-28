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

        Console.WriteLine("🚀 Starting server...\n");

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
            if (e.Data != null) Console.WriteLine($"❌ {e.Data}");
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
                Console.WriteLine("🛑 Stopping previous server...");

                KillProcessTree(_process.Id);

                _process.WaitForExit();
                Thread.Sleep(700);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error stopping process: {ex.Message}");
        }
    }

    private void KillProcessTree(int pid)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "taskkill",
            Arguments = $"/PID {pid} /T /F",
            CreateNoWindow = true,
            UseShellExecute = false
        };

        using var proc = Process.Start(psi);
        proc.WaitForExit();
    }

    private (string, string) Split(string cmd)
    {
        var i = cmd.IndexOf(' ');
        return i == -1 ? (cmd, "") : (cmd[..i], cmd[(i + 1)..]);
    }
}