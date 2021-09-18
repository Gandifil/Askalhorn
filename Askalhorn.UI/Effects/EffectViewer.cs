using Askalhorn.Characters;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Effects
{
    public class EffectViewer: FixPanel
    {
        public EffectViewer(IEffect effect, Anchor anchor, float width, float height) : base(anchor, width, height, false, 8)
        {
            AddChild(new VerticalSpace(4));
            AddChild(new IconViewer(effect, Anchor.AutoCenter, 1F, -1F));
            AddChild(new Paragraph(Anchor.AutoCenter, 1, effect.TurnCount.ToString(), true)
            {
                //TextScaleMultiplier = 0.8f,
            });
            SetHeightBasedOnChildren = true;
        }
    }
}