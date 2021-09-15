using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Utils;
using Newtonsoft.Json;

namespace Askalhorn.Characters
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