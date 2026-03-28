namespace HotReloadX.Core.Models;

public class HotReloadOptions
{
    public string RootPath { get; set; } = "";
    public string BuildCommand { get; set; } = "";
    public string RunCommand { get; set; } = "";
}