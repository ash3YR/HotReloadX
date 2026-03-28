using System;
using System.Diagnostics;

namespace HotReloadX.Core.Services;

public class BuildManager
{
    public bool Execute(string command)
    {
        ConsoleHelper.Info("🔨 Building project...\n");

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

        var process = new Process { StartInfo = psi };

        process.OutputDataReceived += (_, e) =>
        {
            if (e.Data != null) Console.WriteLine(e.Data);
        };

        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data != null) ConsoleHelper.Error(e.Data);
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            ConsoleHelper.Success("✔ Build succeeded\n");
            return true;
        }

        ConsoleHelper.Error("❌ Build failed\n");
        return false;
    }

    private (string, string) Split(string cmd)
    {
        var i = cmd.IndexOf(' ');
        return i == -1 ? (cmd, "") : (cmd[..i], cmd[(i + 1)..]);
    }
}