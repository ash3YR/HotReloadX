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

        ConsoleHelper.Info("🚀 HotReloadX Starting...\n");

        var watcher = new FileWatcherService(options.RootPath);
        var processManager = new ProcessManager();
        var buildManager = new BuildManager();
        var pipeline = new PipelineManager(buildManager, processManager);

        // First run
        pipeline.Trigger(options.BuildCommand, options.RunCommand);

        watcher.OnFilesChanged += () =>
        {
            pipeline.Trigger(options.BuildCommand, options.RunCommand);
        };

        watcher.Start();

        ConsoleHelper.Info("👀 Watching for changes...\n");

        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}