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
        private readonly List<IBag.Pack> Elements = new();

        IReadOnlyCollection<IBag.Pack> IBag.Items => Elements;

        public void Put(IItem item, uint count = 1)
        {
            var founded = Find(item);
            if (founded is null)
                Elements.Add(new IBag.Pack()
                {
                    Item = item,
                    Count = count,
                });
            else
                founded.Count += count;
            OnChanged?.Invoke();
        }

        public IItem Pull(IItem item, uint count = 1)
        {
            var founded = Find(item);
            founded.Count -= count;
            if (founded.Count == 0)
                Elements.Remove(founded);
            OnChanged?.Invoke();
            return item;
        }

        private IBag.Pack Find(IItem item)
        {
            return Elements.Find(x => item.Equals(x.Item));
        }

        public float Weight => Elements.Select(x => x.Count * x.Item.Weight).Sum();
        public event Action OnChanged;
    }
}