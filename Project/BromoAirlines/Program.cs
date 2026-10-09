using Avalonia;
using BromoAirlines.Data;
using System;

namespace BromoAirlines;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // Seeder CLI: dotnet run -- --seed  (tanpa membuka UI)
        if (args.Contains("--seed"))
        {
            var (ok, msg) = Seeder.SeedAsync().GetAwaiter().GetResult();
            Console.WriteLine(msg);
            Environment.Exit(ok ? 0 : 1);
            return;
        }
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
