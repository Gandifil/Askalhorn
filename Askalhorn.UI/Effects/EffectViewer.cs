using Askalhorn.Characters;
using Askalhorn.Characters.Effects;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Effects
{
    public class EffectViewer: FixPanel
    {
        public EffectViewer(TempEffectBind bind, Anchor anchor, float width, float height) : base(anchor, width, height, false, 8)
        {
            AddChild(new VerticalSpace(4));
            AddChild(new IconViewer(bind.Effect, Anchor.AutoCenter, 1F, -1F));
            AddChild(new CustomText(Anchor.AutoCenter, bind.TurnCount.ToString()));
            SetHeightBasedOnChildren = true;
        }
    }
}