using Askalhorn.Core;
using Askalhorn.Text;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Abilities
{
    public class AbilitiesWindow: FixPanel
    {
        private static readonly TextPointer TITLE = new TextPointer("abilityWindow", "Title");
            
        public AbilitiesWindow(Anchor anchor, float width, float height) : base(anchor, width, height, true)
        {
            AddChild(new Paragraph(Anchor.AutoCenter, 1, TITLE.ToString(), true));
            
            var owner = GameProcess.Instance.Player;
            foreach (var ability in owner.Abilities)
                AddChild(new AbilityControlViewer(owner, ability, Anchor.AutoCenter, 1f, .1f));
        }
    }
}