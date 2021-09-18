using Askalhorn.Characters;
using Askalhorn.UI.Inventory;
using MLEM.Ui;

namespace Askalhorn.UI
{
    public class CharacterInventoryViewer: FixPanel
    {
        public CharacterInventoryViewer(ICharacter character, Anchor anchor = Anchor.TopCenter, float width = .9f, float height = .9f): 
            base(anchor, width, height)
        {
            AddChild(new BagViewer(character.Bag, Anchor.CenterRight, 0.5f, 1));
            AddChild(new CostumeViewer(character.Costume, Anchor.CenterLeft, 0.5f, 1));
        }
    }
}