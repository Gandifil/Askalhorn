using System;
using Newtonsoft.Json;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class ObservedLimitedValue<T> : ILimitedValue<IObservedParameter<T>> 
        where T:IComparable<T>, IConvertible, IEquatable<T>
    {
        IObservedParameter<T> ILimitedValue<IObservedParameter<T>>.Current => Current;

        private ObservedParameter<T> _current;

        public ObservedParameter<T> Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                _current.Changed += ResetValue;
            }
        }

        IObservedParameter<T> ILimitedValue<IObservedParameter<T>>.Max => Max;

        private ObservedParameter<T> _max;
        
        [JsonIgnore]
        public ObservedParameter<T> Max 
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                _max.Changed += ResetValue;
                ResetValue();
            }
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
