using System;
using Newtonsoft.Json;

namespace Askalhorn.Common.Mechanics.Utils
{
    class ObservedLimitedValue<T> : ILimitedValue<IObservedParameter<T>> 
        where T:IComparable<T>, IConvertible, IEquatable<T>
    {
        IObservedParameter<T> ILimitedValue<IObservedParameter<T>>.Current => Current;

        IObservedParameter<T> ILimitedValue<IObservedParameter<T>>.Max => Max;
        
        public ObservedParameter<T> Current { get; private set; }
        
        public ObservedParameter<T> Max { get; private set; }

        public ObservedLimitedValue(T value, T max)
        {
            Current = new ObservedParameter<T>(max);
            Current.Changed += ResetValue;
            
            Max = new ObservedParameter<T>(max);
            Max.Changed += ResetValue;
        }

        private readonly T zero = (T) (object) 0;

        private void ResetValue()
        {
            if (Current.Value.CompareTo(zero) < 0)
                Current.Value = zero;
            
            if (Current.Value.CompareTo(Max.Value) > 0)
                Current.Value = Max.Value;
        }

        [JsonIgnore]
        public float Percent => (float)(Current.Value.ToDouble(null) / Max.Value.ToDouble(null));
    }
}
