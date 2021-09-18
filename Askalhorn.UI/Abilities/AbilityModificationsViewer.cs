using Askalhorn.Characters;
using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Style;

namespace Askalhorn.UI.Abilities
{
    public class AbilityModificationsViewer: InvisiblePanel
    {
        private readonly IAbility _ability;

        public AbilityModificationsViewer(IAbility ability, Anchor anchor, float x, float y) : base(anchor, x, y)
        {
            _ability = ability;
            _ability.OnChanged += LightCurrentModification;
            
            int i = 0;
            foreach (var modification in ability.Modifications)
            {
                var buffered = i;
                AddChild(new IconViewer(modification, Anchor.AutoInlineIgnoreOverflow, -1f, 1f)
                {
                    Padding = new Padding(new Padding(), 2),
                    OnPressed = _ => ability.CurrentModification = buffered,
                });
                i++;
            }

            LightCurrentModification();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _ability.OnChanged -= LightCurrentModification;
        }

        public void LightCurrentModification()
        {
            for (int i = 0; i < _ability.Modifications.Count; i++)
            {
                var icon = Children[i] as IconViewer;
                var color = _ability.CurrentModification == i ? Color.Transparent : Color.Gray;
                icon.Color = new StyleProp<Color>(color);
            }
        }
    }
}