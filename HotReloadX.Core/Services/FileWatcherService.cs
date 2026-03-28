using System;
using System.IO;

namespace HotReloadX.Core.Services;

public class FileWatcherService
{
    private readonly string _rootPath;
    private readonly DebounceService _debouncer;
    private FileSystemWatcher? _watcher;

    public event Action? OnFilesChanged;

    public FileWatcherService(string rootPath)
    {
        _rootPath = rootPath;
        _debouncer = new DebounceService(500);
    }

    public void Start()
    {
        var fullPath = Path.GetFullPath(_rootPath);

        ConsoleHelper.Info($"📁 Watching: {fullPath}");

        _watcher = new FileSystemWatcher(fullPath)
        {
            IncludeSubdirectories = true,
            EnableRaisingEvents = true,
            Filter = "*.cs"
        };

        _watcher.Changed += Handle;
        _watcher.Created += Handle;
        _watcher.Deleted += Handle;
        _watcher.Renamed += Handle;
    }

    private void Handle(object sender, FileSystemEventArgs e)
    {
        var path = e.FullPath.Replace("\\", "/").ToLower();

        if (path.Contains("/bin/") || path.Contains("/obj/"))
            return;

        _debouncer.Execute(() =>
        {
            ConsoleHelper.Info("🔥 Change detected → Reloading...");
            OnFilesChanged?.Invoke();
        });
    }
}