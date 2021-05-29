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
            public Point Point;
            public IItem Item;
            public uint Count;
        }

        public readonly List<Element> Elements = new List<Element>();

        public Bag()
        {
            Put(new LifePoition(50));
            Put(new EnergyPoition(50));
            Put(new PoisonPoition(20));
            Put(new PoisonPoition(50));
        }
        
        IReadOnlyCollection<(IItem item, uint count)> IBag.Items => Elements.Select(x => (x.Item, x.Count)).ToList();
        public void Put(IItem item, uint count = 1)
        {
            Elements.Add(new Element()
            {
                Point = new Point(0,0),
                Item = item,
                Count = 1,
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