using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class AbilitiesComponent: GameComponent
    {
        private readonly GameProcessScreen screen;
        private readonly ICharacter character;
        
        public AbilitiesComponent(GameProcessScreen screen, ICharacter character): base(screen.game)
        {
            this.screen = screen;
            this.character = character;
        }

        public override void Initialize()
        {
            base.Initialize();

            var box = new Panel(Anchor.BottomRight, new Vector2(0.7f, 0.1f), Vector2.Zero);
            foreach (var item in character.Abilities)
            {
                var image = new Image(Anchor.AutoInline, new Vector2(0.1F, 0.9F), item.Icon.ToMlem());
                image.CanBeMoused = true;
                image.OnPressed += element =>
                {
                    screen.World.playerController.AddMove(new UseAbilityMove(item));
                };
                var tooltip = new Tooltip(500, item.Name + "\n" + item.Description, image);
                tooltip.MouseOffset = new Vector2(32, -64);
                box.AddChild(image);
            }
            screen.game.UiSystem.Add("abilities", box);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                screen.game.UiSystem.Remove("abilities");
        }
    }
}