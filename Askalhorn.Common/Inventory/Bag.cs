using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Askalhorn.Common.Inventory.Items;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Inventory
{
    internal class Bag: IBag
    {
        private readonly List<Pack> _packs = new();

        IReadOnlyCollection<Pack> IBag.Items => _packs;

        public void Put(IItem item, uint count = 1)
        {
            var founded = Find(item);
            if (founded is null) 
                Add(new Pack(item, count));
            else
                founded.Add(count);
        }

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