using System;
using Newtonsoft.Json;

namespace Askalhorn.Utils
{
    public class ObservedLimitedValue<T> : ILimitedValue<IObservedParameter<T>> 
        where T:IComparable<T>, IConvertible, IEquatable<T>
    {
        IObservedParameter<T> ILimitedValue<IObservedParameter<T>>.Current => Current;
        
        public ObservedParameter<T> Current { get; }

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

        public ObservedLimitedValue(T value)
        {
            Current = new ObservedParameter<T>(value);
            Current.Changed += ResetValue;
        }

        private readonly T zero = (T) (object) 0;

        private T _oldMax = (T) (object) 0;

        private void ResetValue()
        {
            if (Current.Value.CompareTo(zero) < 0)
                Current.Value = zero;
            
            if (Current.Value.CompareTo(Max.Value) > 0)
                Current.Value = Max.Value;
            
            if (_oldMax.CompareTo(zero) != 0)
                if (_oldMax.Equals(Current.Value))
                    Current.Value = Max.Value;
            _oldMax = _max.Value;
        }

        [JsonIgnore]
        public float Percent => (float)(Current.Value.ToDouble(null) / Max.Value.ToDouble(null));

        public override string ToString()
        {
            return $"{Current}/{Max}";
        }
    }
}
