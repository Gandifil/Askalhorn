using System;
using Askalhorn.Logging;
using Askalhorn.Settings;
using Microsoft.Xna.Framework;
using MLEM.Misc;
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
#if DEBUG
                .WriteTo.DevelopConsole()
#endif
                .CreateLogger();
            
            Configuration.Load();
            
            MlemPlatform.Current = new MlemPlatform.DesktopGl<TextInputEventArgs>((w, c) => w.TextInput += c);
            using (var game = new AskalhornGame())
                game.Run();
        }
    }
}
