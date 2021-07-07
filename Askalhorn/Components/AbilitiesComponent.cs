using System;
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
            public IAbility _ability;

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
                _ability = ability;
                var image = new Image(Anchor.Center, new Vector2(1, 1), ability.Icon.ToMlem());
                image.CanBeMoused = true;
                image.OnPressed += _ => Run();
                var tooltip = new Tooltip(500, ability.ToString(), image);
                tooltip.MouseOffset = new Vector2(32, -64);
                Panel.AddChild(image);
            }

            public void Run()
            {
                if (_ability is not null) 
                    World.Instance.playerController.AddMove(new UseAbilityMove(_ability));
            }
        }

        private const int ABILITIES_COUNT = 10;

        private AbilityBox[] boxes = new AbilityBox[ABILITIES_COUNT]; 
        
        public AbilitiesComponent(GameProcessScreen screen, ICharacter character): base(screen.game)
        {
            this.screen = screen;
            this.character = character;
        }

        public override void Initialize()
        {
            base.Initialize();

            var box = new Panel(Anchor.BottomRight, new Vector2(0.7f, 0.1f), Vector2.Zero);

            for (int i = 1; i < ABILITIES_COUNT; i++)
                boxes[i] = new AbilityBox(box, i);
            boxes[0] = new AbilityBox(box, 0);
            
            for (int i = 0; i < 3; i++)
                boxes[i+1].SetEffect(character.Abilities.ElementAt(i));
            
            screen.game.UiSystem.Add("abilities", box);
        }

        public void Run(int index)
        {
            if (index < 0)
                throw new ArgumentException("Index must be greater than or equal zero");
            
            if (index >= ABILITIES_COUNT)
                throw new ArgumentException($"Index must be lower than count of ability's block ({ABILITIES_COUNT})");
            
            boxes[index].Run();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                screen.game.UiSystem.Remove("abilities");
        }
    }
}