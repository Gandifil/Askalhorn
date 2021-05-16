using System;

namespace Askalhorn.Common.Maths
{
    public class ObservedParameter : IObservedParameter
    {
        private int _value = 0;

        public int Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    OnChanged();
                }
            }
        }

        public ObservedParameter(int x)
        {
            _value = x;
        }

        private void OnChanged()
        {
            Changed?.Invoke();
        }

        public event Action Changed;

        public static implicit operator int(ObservedParameter p)
        {
            return p._value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
