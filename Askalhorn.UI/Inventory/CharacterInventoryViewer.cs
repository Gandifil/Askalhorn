using Askalhorn.Characters;
using Askalhorn.UI.Input;
using MLEM.Ui;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Inventory
{
    public class CharacterInventoryViewer: FixPanel
    {
        public CharacterInventoryViewer(ICharacter character, Anchor anchor = Anchor.TopCenter, float width = .9f, float height = .9f): 
            base(anchor, width, height)
        {
            InputListeners.Input.MouseListener.Push(new MouseListener());
            AddChild(new BagViewer(character.Bag, Anchor.CenterRight, 0.5f, 1));
            AddChild(new CostumeViewer(character.Costume, Anchor.CenterLeft, 0.5f, 1));
        }

        public override void Dispose()
        {
            InputListeners.Input.MouseListener.Pop();
            
            base.Dispose();
        }
    }
}