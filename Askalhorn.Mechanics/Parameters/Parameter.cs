using System;

namespace Askalhorn.Mechanics.Parameters
{
    public class Parameter
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

        private void OnChanged()
        {
            Changed?.Invoke();
        }

        public event Action Changed;

        public static implicit operator int(Parameter p)
        {
            return p._value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
