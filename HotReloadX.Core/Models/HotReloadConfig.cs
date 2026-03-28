namespace HotReloadX.Core.Models;

public class HotReloadConfig
{
    public string Root { get; set; } = "";
    public string Build { get; set; } = "";
    public string Run { get; set; } = "";
    public List<string> Ignore { get; set; } = new();
}