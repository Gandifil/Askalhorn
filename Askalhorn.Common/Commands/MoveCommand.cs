using System;
using Askalhorn.Common.Geography.Local;
using Serilog;

namespace Askalhorn.Common.Commands
{
    internal class MoveCommand: ICommand
    {
        public void Run(string[] args)
        {
            var position = new Position(Convert.ToUInt32(args[0]), Convert.ToUInt32(args[1]));
            
            Log.Debug("Player moved to {position}", position);

            World.Instance.Player.Position = position;
        }
    }
}