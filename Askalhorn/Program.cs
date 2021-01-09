using System;

namespace Askalhorn
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new AskalhornGame())
                game.Run();
        }
    }
}
