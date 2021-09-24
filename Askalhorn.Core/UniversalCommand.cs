using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Askalhorn.Common;
using Microsoft.Xna.Framework.Input;

namespace Askalhorn.Core
{
    public class UniversalCommand: ICommand
    {
        private readonly Dictionary<string, ICommand> _commands = new();

        public UniversalCommand()
        {
            foreach (var pair in HarvestAllCommands())
                _commands.Add(pair.Key, pair.Value);
            
            foreach (var pair in HarvestAllImpacts())
                _commands.Add(pair.Key, pair.Value);
        }

        private Dictionary<string, ICommand> HarvestAllImpacts()
        {
            var impacts = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName?.Contains("Askalhorn") ?? false)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IImpact).IsAssignableFrom(t));

            return impacts
                .SelectMany(x => x.GetConstructors())
                .Where(c => Attribute.IsDefined(c, typeof(CommandConstructorAttribute)))
                .ToDictionary(
                    c => c.DeclaringType?.Name.Replace("Impact", ""),
                    c => new DelegatingCommand(c) as ICommand
                );
        }

        private Dictionary<string, ICommand> HarvestAllCommands()
        {
            var commandType = typeof(ICommand);
            
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName?.Contains("Askalhorn") ?? false)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && commandType.IsAssignableFrom(t) && t != GetType() && t != typeof(DelegatingCommand))
                .ToDictionary(
                    x => x.Name.Replace("Command", ""), 
                    x => Activator.CreateInstance(x) as ICommand
                    );
        }
        
        public void Run(object target, string[] args)
        {
            var command = _commands[args[0]];
            var arguments = args.TakeLast(args.Length - 2);
            
            command.Run(FindTarget(args[1]), arguments.ToArray());
        }
        
        private object FindTarget(string name)
        {
            if (name == "me" || name == "this")
                return GameProcess.Instance.Player;

            return null;
        }
    }
}