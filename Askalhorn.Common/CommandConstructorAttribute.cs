using System;

namespace Askalhorn.Common
{
    /// <summary>
    /// Instructs the <see cref="UniversalCommand"/> to use the specified constructor when search commands.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class CommandConstructorAttribute: Attribute
    {
        
    }
}