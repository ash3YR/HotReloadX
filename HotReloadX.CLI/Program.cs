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
        var process = new ProcessManager();
        var build = new BuildManager();
        var pipeline = new PipelineManager(build, process);

        pipeline.Trigger(options.BuildCommand, options.RunCommand);

        watcher.OnFilesChanged += () =>
        {
            pipeline.Trigger(options.BuildCommand, options.RunCommand);
        };

        watcher.Start();

        Console.WriteLine("👀 Watching... Press Ctrl+C to exit.");

        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}