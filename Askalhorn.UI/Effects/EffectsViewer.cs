using System.Linq;
using Askalhorn.Characters;
using Askalhorn.Characters.Effects;
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
            foreach (var bind in _character.EffectPool.Binds
                .Select(x => x as TempEffectBind)
                .Where(x => x is not null))
                AddChild(new EffectViewer(bind, Anchor.AutoInlineIgnoreOverflow, 0.03F, 1F));
        }
    }
}