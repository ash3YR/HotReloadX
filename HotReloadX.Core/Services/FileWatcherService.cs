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

        Console.WriteLine($"📁 Watching: {fullPath}");

        _watcher = new FileSystemWatcher(fullPath)
        {
            IncludeSubdirectories = true,
            EnableRaisingEvents = true,
            NotifyFilter = NotifyFilters.FileName
                         | NotifyFilters.LastWrite
                         | NotifyFilters.DirectoryName
        };

        _watcher.Changed += OnChange;
        _watcher.Created += OnChange;
        _watcher.Deleted += OnChange;
        _watcher.Renamed += OnRename;
    }

    private void OnChange(object sender, FileSystemEventArgs e)
    {
        if (IsIgnored(e.FullPath)) return;

        _debouncer.Execute(() =>
        {
            OnFilesChanged?.Invoke();
        });
    }

    private void OnRename(object sender, RenamedEventArgs e)
    {
        if (IsIgnored(e.FullPath)) return;

        _debouncer.Execute(() =>
        {
            OnFilesChanged?.Invoke();
        });
    }

    private bool IsIgnored(string path)
    {
        return path.Contains("bin") ||
               path.Contains("obj") ||
               path.Contains(".git") ||
               path.Contains("node_modules");
    }
}