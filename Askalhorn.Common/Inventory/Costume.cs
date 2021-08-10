using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Mechanics.Interpretators;

namespace Askalhorn.Common.Inventory
{
    public class Costume
    {
        public readonly Slot Head = new Slot(IItem.PurposeType.Head);
        public readonly Slot Body = new Slot(IItem.PurposeType.Body);
        public readonly Slot Hands = new Slot(IItem.PurposeType.Hands);
        public readonly Slot Boots = new Slot(IItem.PurposeType.Boots);
        public readonly Slot Weapon = new Slot(IItem.PurposeType.Weapon);
        public readonly Slot Shield = new Slot(IItem.PurposeType.Shield);

        public readonly Slot Cloak = new Slot(IItem.PurposeType.Cloak);
        public readonly Slot Amulet = new Slot(IItem.PurposeType.Amulet);

        public readonly Dictionary<IItem.PurposeType, Slot> Clothes =
            ((IItem.PurposeType[]) Enum.GetValues(typeof(IItem.PurposeType)))
            .Select(x => new Slot(x))
            .ToDictionary(x => x.Type, x => x);

        public readonly Slot SecondRing =new Slot(IItem.PurposeType.Ring);

        public readonly Slot[] Rings =
        {
            new Slot(IItem.PurposeType.Ring),
            new Slot(IItem.PurposeType.Ring)
        };

        public readonly Slot Bracelet = new Slot(IItem.PurposeType.Bracelet);
    }
} 