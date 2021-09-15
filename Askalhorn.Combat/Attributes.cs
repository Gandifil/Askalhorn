using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Askalhorn.Utils;

namespace Askalhorn.Combat
{
    public class Attributes<T>: IAttributes<T> where T : Enum
    {
        ILinearParameter<int> IAttributes<T>.this[T index] => table[index];

        public LinearParameter<int> this[T index] => table[index];
        
        [JsonIgnore]
        public Dictionary<T, ObservedParameter<int>> BaseTable =>
            table.ToDictionary( 
                p=> p.Key,
                p=> p.Value.Base);
        
        private readonly Dictionary<T, LinearParameter<int>> table;

        public Attributes(Dictionary<T, ObservedParameter<int>> baseTable)
        {
            this.table = baseTable.ToDictionary( 
                p=> p.Key,
                p=> new LinearParameter<int>
                {
                    Base = p.Value,
                });
        }
    }
}