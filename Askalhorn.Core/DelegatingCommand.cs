using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Askalhorn.Common;

namespace Askalhorn.Core
{
    public class DelegatingCommand: ICommand
    {
        private readonly ConstructorInfo _constructor;

        public DelegatingCommand(ConstructorInfo constructor)
        {
            _constructor = constructor;
        }
        
        public void Run(object target, string[] args)
        {
            
            var impact = _constructor.Invoke(CastArguments(args)) as IImpact;
            impact.On(target);
        }

        private object?[]? CastArguments(string[] args)
        {
            var queue = new Queue<string>(args);
            var results = new List<object>();
            foreach (var parameter in _constructor.GetParameters())
            {
                if (queue.Any())
                {
                    var typeName = parameter.ParameterType.Name;
                    switch (typeName)
                    {
                        case "String":
                            results.Add(queue.Dequeue());
                            break;
                        case "UInt32":
                            results.Add(Convert.ToUInt32(queue.Dequeue()));
                            break;
                        default:
                            throw new ArgumentException("Unknown type " + typeName);
                            break;
                    }
                }
                
                else
                {
                    if (parameter.IsOptional)
                        results.Add(parameter.DefaultValue);
                    else
                        throw new ArgumentException("Too small count arguments");
                }

            }

            return queue.Any() ? throw new ArgumentException("Too many arguments") : results.ToArray();
        }
    }
}