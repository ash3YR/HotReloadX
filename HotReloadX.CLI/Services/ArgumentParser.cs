using HotReloadX.Core.Models;

namespace HotReloadX.CLI.Services;

public class ArgumentParser
{
    public static HotReloadOptions Parse(string[] args)
    {
        var options = new HotReloadOptions();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--root":
                    options.RootPath = args[++i];
                    break;

                case "--build":
                    options.BuildCommand = args[++i];
                    break;

                case "--exec":
                    options.RunCommand = args[++i];
                    break;
            }
        }

        return options;
    }
}