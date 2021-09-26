using System;
using Askalhorn.Characters;
using Askalhorn.Combat;
using Askalhorn.Text;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Characters
{
    public class CharacterWindow: FixPanel
    {
        public CharacterWindow(ICharacter character, Anchor anchor = Anchor.CenterRight, float width = .45f, float height = .9f): 
            base(anchor, width, height)
        {
            AddChild(new CharacterViewer(character, Anchor.TopLeft, .5f, .5f));
            AddChild(new ProtectionViewer(character, Anchor.TopRight, .5f, .5f));
            AddChild(new PrimaryAttributesViewer(character, Anchor.BottomLeft, .5f, .5f));
            AddChild(new SecondaryAttributesViewer(character, Anchor.BottomRight, .5f, .5f));
        }
    }
}