using HotReloadX.CLI.Services;
using HotReloadX.Core.Services;

class Program
{
    static void Main(string[] args)
    {
        var options = ArgumentParser.Parse(args);

        if (string.IsNullOrWhiteSpace(options.RootPath) ||
            string.IsNullOrWhiteSpace(options.BuildCommand) ||
            string.IsNullOrWhiteSpace(options.RunCommand))
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("hotreloadx --root <path> --build \"command\" --exec \"command\"");
            return;
        }

        Console.WriteLine("🚀 HotReloadX Starting...\n");

        var watcher = new FileWatcherService(options.RootPath);

        watcher.OnFilesChanged += () =>
        {
            Console.WriteLine("🔥 File change pipeline triggered!");
        };

        watcher.Start();

        Console.WriteLine("👀 Watching for changes... Press Ctrl+C to exit.");

        // Keep app alive
        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}