using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Inventory.Items;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Inventory
{
    internal class Bag: IBag
    {
        public struct Element
        {
            public IItem Item;
            public uint Count;
        }

        public readonly List<Element> Elements = new List<Element>();
        
        IReadOnlyCollection<(IItem item, uint count)> IBag.Items => Elements.Select(x => (x.Item, x.Count)).ToList();
        public void Put(IItem item, uint count = 1)
        {
            Elements.Add(new Element()
            {
                Item = item,
                Count = count,
            });
        }

        public IItem Pull(IItem item, uint count = 1)
        {
            Elements.RemoveAll(x => x.Item == item);
            return item;
        }

        public float Weight => Elements.Select(x => x.Count * x.Item.Weight).Sum();
    }
}