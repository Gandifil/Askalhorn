using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Inventory
{
    public class Costume
    {
        public readonly Dictionary<ItemPurpose, Slot> Clothes =
            ((ItemPurpose[]) Enum.GetValues(typeof(ItemPurpose)))
            .Select(x => new Slot(x))
            .ToDictionary(x => x.Type, x => x);

        public readonly Slot SecondRing = new Slot(ItemPurpose.Ring);

        public Costume()
        {
            foreach (var pair in Clothes)
            {
                pair.Value.OnPutOn += OnPutOn;
                pair.Value.OnTakeOff += TakeOffItem;
            }
            SecondRing.OnPutOn += PutOnItem;
            SecondRing.OnTakeOff += TakeOffItem;
        }

        private void OnPutOn(Slot obj)
        {
            PutOnItem?.Invoke(obj);
        }

        public event Action<Slot> PutOnItem;        
        public event Action<Slot> TakeOffItem;

        //
        // public readonly Slot Head = new Slot(ItemPurpose.Head);
        //
        // public readonly Slot Body = new Slot(ItemPurpose.Body);
        // public readonly Slot Hands = new Slot(ItemPurpose.Hands);
        // public readonly Slot Boots = new Slot(ItemPurpose.Boots);
        // public readonly Slot Weapon = new Slot(ItemPurpose.Weapon);
        // public readonly Slot Shield = new Slot(ItemPurpose.Shield);
        //
        // public readonly Slot Cloak = new Slot(ItemPurpose.Cloak);
        // public readonly Slot Amulet = new Slot(ItemPurpose.Amulet);
        //
        //
        // public readonly Slot[] Rings =
        // {
        //     new Slot(ItemPurpose.Ring),
        //     new Slot(ItemPurpose.Ring)
        // };
        //
        // public readonly Slot Bracelet = new Slot(ItemPurpose.Bracelet);
    }
} 