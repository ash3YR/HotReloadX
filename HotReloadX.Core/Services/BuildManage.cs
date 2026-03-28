using System;
using System.Diagnostics;

namespace HotReloadX.Core.Services;

public class BuildManager
{
    public bool Execute(string command)
    {
        Console.WriteLine("🔨 Building project...\n");

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
            if (e.Data != null) Console.WriteLine($"❌ {e.Data}");
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            Console.WriteLine("✔ Build succeeded\n");
            return true;
        }

        Console.WriteLine("❌ Build failed\n");
        return false;
    }

    private (string, string) Split(string cmd)
    {
        var i = cmd.IndexOf(' ');
        return i == -1 ? (cmd, "") : (cmd[..i], cmd[(i + 1)..]);
    }
}