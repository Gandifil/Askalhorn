using System;
using System.Collections.Generic;
using System.Text;

namespace Askalhorn.Mechanics.Parameters
{
    class Attributes<T> : IAttributes<T>
    {
        public Dictionary<T, LinearParameter> Attrs { get; private set; } = new Dictionary<T, LinearParameter>();

        public LinearParameter this[T index] 
        { 
            get 
            {
                return Attrs[index];
            } 
            set 
            {
                Attrs[index] = value;
            } 
        }

        IModifiedParameter IAttributes<T>.this[T index] => Attrs[index];

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"{typeof(T).FullName}:");

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (Attrs.TryGetValue(item, out var value))
                    result.AppendLine($"\t{item}: {value}");
                else
                    result.AppendLine($"\t{item}: null");
            }

            return result.ToString();
        }
    }
}
