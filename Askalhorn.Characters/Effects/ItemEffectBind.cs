using Askalhorn.Characters.Items;
using Askalhorn.Inventory;

namespace Askalhorn.Characters.Effects
{
    public class ItemEffectBind:EffectBind
    {
        public ItemEffectBind(Slot slot): 
            base((slot.Item.InnerItem as IPuttable)?.Effect)
        {
            slot.OnTakeOff += OnTakeOff;
        }

        private void OnTakeOff(Slot slot)
        {
            slot.OnTakeOff -= OnTakeOff;
            Remove();
        }
    }
}