using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class AbilitiesComponent: GameComponent
    {
        private readonly AskalhornGame game;
        private readonly ICharacter character;
        
        public AbilitiesComponent(AskalhornGame game, ICharacter character): base(game)
        {
            this.game = game;
            this.character = character;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            var box = new Panel(Anchor.BottomRight, new Vector2(0.7f, 0.1f), Vector2.Zero);
                        foreach (var item in character.Abilities)
                        {
                            var image = new Image(Anchor.Center, new Vector2(0.6F, 0.75F), new MLEM.Textures.TextureRegion(item.Icon));
                            image.CanBeMoused = true;
                            image.OnPressed += element =>
                            {
                                // movements.AvailableAbilities = world.Characters.Select(x =>
                                //     new UseAbilityMove(item)
                                //     {
                                //         Target = x,
                                //     });
                            };
                            var tooltip = new Tooltip(200, item.Name + "\n" + item.Description, image);
                            tooltip.MouseOffset = new Vector2(32, -64);
                            box.AddChild(image);
                        }
            game.UiSystem.Add("abilities", box);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                game.UiSystem.Remove("abilities");
        }
    }
}