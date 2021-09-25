using System;
using Askalhorn.Inventory.Items;
using Newtonsoft.Json;

namespace Askalhorn.Inventory
{
    public sealed class Pack
    {
        public readonly IItem Item;

        public uint Count { get; private set; }

        public event Action OnCountChange;

        public event Action<Pack> OnCountSetNull;

        [JsonConstructor]
        public Pack(IItem item, uint count = 1)
        {
            Item = item;
            Count = count;
        }

        public void Add(uint count = 1)
        {
            Count += count;
            OnCountChange?.Invoke();
        }

        public void Remove(uint count = 1)
        {
            Count -= count;
            OnCountChange?.Invoke();
            if (Count == 0)
                OnCountSetNull?.Invoke(this);
        }
    }
}