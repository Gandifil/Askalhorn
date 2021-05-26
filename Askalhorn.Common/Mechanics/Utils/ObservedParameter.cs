using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class ObservedParameter<T> : IObservedParameter<T> where T:IEquatable<T>
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                    OnChanged();
                }
            }
        }

        public ObservedParameter(T x)
        {
            _value = x;
        }

        private void OnChanged()
        {
            Changed?.Invoke();
        }

        public event Action Changed;

        public static implicit operator T(ObservedParameter<T> p)
        {
            return p._value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
