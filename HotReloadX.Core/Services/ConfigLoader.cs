using System.Text.Json;
using HotReloadX.Core.Models;

namespace HotReloadX.Core.Services;

public class ConfigLoader
{
    public static HotReloadConfig Load(string path = "hotreload.json")
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("⚠️ No config file found, using CLI args");
            return new HotReloadConfig();
        }

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<HotReloadConfig>(json)!;
    }
}