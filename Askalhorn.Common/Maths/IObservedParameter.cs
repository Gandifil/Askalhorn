using System;

namespace Askalhorn.Common.Maths
{
    public interface IObservedParameter<T>
    {
        T Value { get; set; }
        event Action Changed;
    }
}