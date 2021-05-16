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
                .Enrich.FromLogContext()
                .WriteTo.GameLog()
                .CreateLogger();
            
            using (var game = new AskalhornGame())
                game.Run();
        }
    }
}
