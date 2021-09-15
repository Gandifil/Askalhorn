using System;

namespace Askalhorn.Utils
{
    public interface IObservedParameter<out T>
    {
        T Value { get; }
        event Action Changed;
    }
}