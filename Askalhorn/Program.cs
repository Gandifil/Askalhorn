using System;
using Askalhorn.Logging;
using Serilog;

namespace Askalhorn
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.GameLog("{Message:lj}")
                .WriteTo.DevelopConsole()
                .CreateLogger();
            
            using (var game = new AskalhornGame())
                game.Run();
        }
    }
}
