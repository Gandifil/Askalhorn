using Askalhorn.Characters;
using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Abilities
{
    public class AbilityControlViewer: FixPanel
    {
        public AbilityControlViewer(ICharacter owner, IAbility ability, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new AbilityViewer(owner, ability, Anchor.CenterLeft, -1, 1F));
            
            AddChild(new Paragraph(Anchor.TopRight, 500, ability.Name, true));
            
            AddChild(new AbilitySkillProgressBar(ability, Anchor.BottomRight, 0.8f, 0.5f));
            
            AddChild(new AbilityModificationsViewer(ability, Anchor.TopCenter, 0.3f, 0.5f));
        }
    }
}