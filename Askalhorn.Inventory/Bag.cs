using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Inventory
{
    public class Bag
    {
        private readonly List<Pack> _packs = new();

        public IReadOnlyCollection<Pack> Items => _packs;

        public void Put(IItem item, uint count = 1)
        {
            var founded = Find(item);
            if (founded is null) 
                Add(new Pack(item, count));
            else
                founded.Add(count);
        }

        public bool IsEmpty => !_packs.Any();

        private void Add(Pack pack)
        {
            pack.OnCountSetNull += Remove;
            _packs.Add(pack);
            OnChanged?.Invoke();
        }

        private void Remove(Pack pack)
        {
            _packs.Remove(pack);
            OnChanged?.Invoke();
        }

        public IItem Pull(IItem item, uint count = 1)
        {
            var founded = Find(item);
            founded.Remove(count);
            return item;
        }

        private Pack Find(IItem item)
        {
            return _packs.Find(x => item.Equals(x.Item));
        }

        public float Weight => _packs.Select(x => x.Count * x.Item.Weight).Sum();
        public event Action OnChanged;
    }
}