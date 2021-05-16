using System;

namespace Askalhorn.Common.Maths
{
    public interface IObservedParameter
    {
        int Value { get; set; }
        event Action Changed;
    }
}