using Askalhorn.Characters.Effects;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Inventory.Items;
using Askalhorn.Text;

namespace Askalhorn.Characters.Items
{
    public class Weapon: PuttableItem
    {
        public uint DamageValue { get; set; }

        public DamageType DamageType { get; set; }

        public IImpact StrikeImpact { get; set; }

        protected override string PreDescription => $@"{base.PreDescription}
Урон: {DamageValue} ({new EnumTextPointer<DamageType>(DamageType)
{
    GrammaticalCase = GrammaticalCase.Genitive}})";
        
        public override ItemPurpose Type => ItemPurpose.Weapon;
    }
}