using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    public interface IObservedParameter<out T>
    {
        T Value { get; }
        event Action Changed;
    }
}