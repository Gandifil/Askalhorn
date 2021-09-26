using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Characters.Items
{
    public class Weapon: Item
    {
        public uint DamageValue { get; set; }

        public DamageTypes DamageType { get; set; }

        public IImpact StrikeImpact { get; set; }

        protected override string PreDescription => $@"{base.PreDescription}
Урон: {DamageValue} ({DamageType})";
        
        public override ItemPurpose Type => ItemPurpose.Weapon;
    }
}