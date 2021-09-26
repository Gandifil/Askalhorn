using Askalhorn.Characters.Effects;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Characters.Items
{
    public class PuttableItem: Item, IPuttable
    {
        public IEffect Effect { get; set; }
    }
}