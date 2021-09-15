using AmbrosiaGame.Screens;
using Askalhorn.Characters;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class EffectsComponent: GameComponent
    {
        private readonly GameProcessScreen screen;
        private readonly ICharacter character;
        private Panel effectsBox;
        
        public EffectsComponent(GameProcessScreen screen, ICharacter character): base(screen.game)
        {
            this.screen = screen;
            this.character = character;
        }

        public override void Initialize()
        {
            base.Initialize();
            effectsBox = new Panel(Anchor.TopLeft, new Vector2(1, 0.07f), Vector2.Zero);
            effectsBox.Texture = null;
            screen.game.UiSystem.Add("effects", effectsBox);
        }

        public void Update()
        {
            Clear();
            foreach (var item in character.Effects)
                effectsBox.AddChild(CreateEffectView(item));
        }

        private static Element CreateEffectView(IEffect effect)
        {
            var box = new Panel(Anchor.AutoInlineIgnoreOverflow, new Vector2(0.03F, 1F), Vector2.Zero);
            var image = new Image(Anchor.AutoCenter, new Vector2(0.8F, 0.8F), effect.TextureRegion.ToMlem());
            image.CanBeMoused = true;
            var tooltip = new Tooltip(500, effect.Description, image);
            tooltip.MouseOffset = new Vector2(32, -64);
            box.AddChild(image);
            box.AddChild(new Paragraph(Anchor.AutoCenter, 20, effect.TurnCount.ToString())
            {
                TextScaleMultiplier = 0.8f,
            });
            return box;
        }

        private void Clear()
        {
            effectsBox.RemoveChildren();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                screen.game.UiSystem.Remove("effects");
        }
    }
}