namespace HotReloadX.Core.Models;

public class HotReloadOptions
{
    public string RootPath { get; set; } = string.Empty;
    public string BuildCommand { get; set; } = string.Empty;
    public string RunCommand { get; set; } = string.Empty;
}