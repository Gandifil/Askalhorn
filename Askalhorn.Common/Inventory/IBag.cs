using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Askalhorn.Common.Inventory
{
    public interface IBag
    {
        public class Pack
        {
            public IItem Item;
            public uint Count;
        }
        
        IReadOnlyCollection<Pack> Items { get; }

        void Put(IItem item, uint count = 1);
        IItem Pull(IItem item, uint count = 1);
        float Weight { get; }
        public event Action OnChanged;
    }
}