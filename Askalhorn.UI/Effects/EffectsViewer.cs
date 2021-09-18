using Askalhorn.Characters;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Effects
{
    public class EffectsViewer: InvisiblePanel
    {
        private readonly ICharacter _character;

        public EffectsViewer(ICharacter character, Anchor anchor, float x, float y) : base(anchor, x, y)
        {
            _character = character;
        }

        public void Update()
        {
            RemoveChildren();
            foreach (var item in _character.Effects)
                AddChild(new EffectViewer(item, Anchor.AutoInlineIgnoreOverflow, 0.03F, 1F));
        }
    }
}