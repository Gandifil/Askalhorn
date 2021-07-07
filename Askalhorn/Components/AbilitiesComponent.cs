using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Mechanics;
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

        private class AbilityBox
        {
            public readonly Panel Panel;

            public AbilityBox(Panel parent, int index)
            {
                Panel = new Panel(Anchor.AutoInlineIgnoreOverflow, new Vector2(0.1f, 1), Vector2.Zero);
                Panel.AddChild(new Paragraph(Anchor.BottomRight, 20, index.ToString())
                {
                    TextScaleMultiplier = 0.7f,
                });
                parent.AddChild(Panel);
            }

            public void SetEffect(IAbility ability)
            {
                var image = new Image(Anchor.Center, new Vector2(1, 1), ability.Icon.ToMlem());
                image.CanBeMoused = true;
                image.OnPressed += element =>
                {
                    World.Instance.playerController.AddMove(new UseAbilityMove(ability));
                };
                var tooltip = new Tooltip(500, ability.Name + "\n" + ability.Description, image);
                tooltip.MouseOffset = new Vector2(32, -64);
                Panel.AddChild(image);
            }
        }

        private AbilityBox[] boxes = new AbilityBox[10]; 
        
        public AbilitiesComponent(GameProcessScreen screen, ICharacter character): base(screen.game)
        {
            this.screen = screen;
            this.character = character;
        }

        public override void Initialize()
        {
            base.Initialize();

            var box = new Panel(Anchor.BottomRight, new Vector2(0.7f, 0.1f), Vector2.Zero);

            for (int i = 1; i < 10; i++)
                boxes[i] = new AbilityBox(box, i);
            boxes[0] = new AbilityBox(box, 0);

            boxes[1].SetEffect(character.Abilities.ElementAt(1));
            boxes[2].SetEffect(character.Abilities.ElementAt(2));
            boxes[3].SetEffect(character.Abilities.ElementAt(3));
            
            screen.game.UiSystem.Add("abilities", box);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                screen.game.UiSystem.Remove("abilities");
        }
    }
}