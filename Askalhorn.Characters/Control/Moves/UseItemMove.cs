using System.Linq;
using Askalhorn.Inventory;
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
            return character.Bag.Items.Any(x => x.Item == Item);
        }

        public void Make(Character character)
        {
            var itemName = Item.Name;
            Log.Information("{Name} использовал {itemName}", character.Name, itemName);
            character.Bag.Pull(Item).Impact?.On(character);
        }
    }
}