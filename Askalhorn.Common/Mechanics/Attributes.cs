using System;
using System.Collections.Generic;
using Askalhorn.Common.Mechanics.Utils;

namespace Askalhorn.Common.Mechanics
{
    public class Attributes<T>: IAttributes<T> where T : Enum
    {
        IModifiedParameter<int> IAttributes<T>.this[T index] => table[index];

        public LinearParameter<int> this[T index] => table[index];

        private readonly Dictionary<T, LinearParameter<int>> table;

        public Attributes(Dictionary<T, LinearParameter<int>> table)
        {
            this.table = table;
        }
    }
}