using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    public interface IObservedParameter<T>
    {
        T Value { get; set; }
        event Action Changed;
    }
}