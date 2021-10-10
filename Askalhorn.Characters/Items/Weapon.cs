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

        protected override string PreDescription => 
$@"Урон: {DamageValue} ({EnumTextPointer<DamageType>.Get(DamageType, GrammaticalCase.Genitive)})
{base.PreDescription}";
        
        public override ItemPurpose Type => ItemPurpose.Weapon;
    }
}