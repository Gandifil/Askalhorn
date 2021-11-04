using System.Linq;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Serilog;

namespace Askalhorn.Characters.Control.Moves
{
    public class UseItemMove: IMove
    {
        public readonly IItem Item;

        public UseItemMove(IItem Item)
        {
            this.Item = Item;
        }
        
        public bool IsValid(ICharacter character)
        {
            return Item.Impact is not null && character.Bag.Packs.Any(x => x.Item == Item);
        }

        public void Make(Character character)
        {
            var itemName = Item.Name;
            Log.Information("{Name} использовал {itemName}", character.Name, itemName);
            character.Bag.Pull(Item).Impact?.On(character);
        }
    }
}