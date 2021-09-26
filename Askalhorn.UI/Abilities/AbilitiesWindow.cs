using Askalhorn.Core;
using Askalhorn.Text;
using Askalhorn.UI.Input;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Abilities
{
    public class AbilitiesWindow: FixPanel
    {
        private static readonly TextPointer TITLE = new TextPointer("abilityWindow", "Title");
            
        public AbilitiesWindow(Anchor anchor, float width, float height) : base(anchor, width, height, true)
        {
            InputListeners.Input.MouseListener.Push(new MouseListener());
            AddChild(new CustomText(Anchor.AutoCenter, TITLE.ToString()));
            
            var owner = GameProcess.Instance.Player;
            foreach (var ability in owner.Abilities)
                AddChild(new AbilityControlViewer(owner, ability, Anchor.AutoCenter, 1f, .1f));
        }

        public override void Dispose()
        {
            InputListeners.Input.MouseListener.Pop();
            
            base.Dispose();
        }
    }
}