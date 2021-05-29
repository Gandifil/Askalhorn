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
            AddItem(new LifePoition(50));
            AddItem(new EnergyPoition(50));
            AddItem(new PoisonPoition(20));
            AddItem(new PoisonPoition(50));
        }

        public void AddItem(IItem item)
        {
            Elements.Add(new Element()
            {
                Point = new Point(0,0),
                Item = item,
                Count = 1,
            });
        }

        IReadOnlyCollection<(IItem item, uint count)> IBag.Items => Elements.Select(x => (x.Item, x.Count)).ToList();

        public float Weight => Elements.Select(x => x.Count * x.Item.Weight).Sum();
    }
}