using System;
using Askalhorn.Characters;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;

namespace Askalhorn.Core.Commands
{
    internal class StartQuestCommand: ICommand
    {
        public void Run(string[] args)
        {
            if (args.Length == 0)
                throw new IndexOutOfRangeException("Args must exists");
            
            if (args.Length > 2)
                throw new IndexOutOfRangeException("Args must have only 2 elements");
            
            new StartQuestImpact(args[0], args.Length == 2 ? args[1] : null)
                .On((Character)GameProcess.Instance.Player);
        }
    }
}