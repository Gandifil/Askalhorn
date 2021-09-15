using System;
using System.Collections.Generic;
using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Characters;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Common;
using Askalhorn.Core;
using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

namespace Askalhorn.Components
{
    public class AbilitiesComponent: GameComponent
    {
        private readonly GameProcessScreen screen;
        private readonly ICharacter character;

        private const int ABILITIES_COUNT = 10;

        private AbilityBox[] boxes = new AbilityBox[ABILITIES_COUNT]; 

        private class AbilityBox: IDisposable
        {
            private readonly GameProcessScreen screen;
            public readonly Panel Panel;
            public Image Image;
            public IAbility _ability;
            private readonly ICharacter character;
            private Tooltip _tooltip;
            private UseAbilityMove _move;

            public AbilityBox(GameProcessScreen screen, Panel parent, int index, ICharacter character)
            {
                this.screen = screen;
                Panel = new FixPanel(Anchor.AutoInlineIgnoreOverflow, 0.1f, 1);
                Panel.AddChild(new Paragraph(Anchor.BottomRight, 20, index.ToString())
                {
                    TextScaleMultiplier = 0.7f,
                });
                //parent.AddChild(new VerticalSpace(5));
                parent.AddChild(Panel);

                this.character = character;
                character.HP.Current.Changed += UpdateMagic;
            }

            private void UpdateMagic()
            {
                if (_ability is not null)
                    Image.Color = new StyleProp<Color>(!_move.IsEnoughMagic(character) ? Color.Red : Color.Transparent);
            }

            public void SetEffect(IAbility ability)
            {
                _ability = ability;

                _move = new UseAbilityMove
                {
                    Ability = _ability,
                };
                
                Image = new Image(Anchor.Center, new Vector2(1, 1), ability.Icon.ToMlem());
                Image.CanBeMoused = true;
                Image.OnPressed += _ => Run();
                Image.OnDrawn += (element, time, batch, alpha) =>
                {
                    if (!ability.IsReady)
                        batch.DrawCircle(element.DisplayArea.Center, 
                            64.0f * ((float)ability.CoolDownTimer / ability.CoolDown), 50, Color.Red);
                };
                _tooltip = new Tooltip(500, ability.ToString(), Image);
                _tooltip.MouseOffset = new Vector2(32, -64);
                ability.OnChange += Update;
                Panel.AddChild(Image);
                UpdateMagic();
            }

            private void Update()
            {
                if (_ability is not null)
                    _tooltip.Paragraph.Text = _ability.ToString();
            }

            public void Run()
            {
                if (_ability is not null)
                {
                    if (_ability.Type == IAbility.TargetType.Self)
                    {
                        GameProcess.Instance.Player.Make(_move);
                        screen.movements.AvailableAbilities = new List<UseAbilityMove>();
                    }

                    if (_ability.Type == IAbility.TargetType.Character)
                    {
                        screen.movements.AvailableAbilities = screen
                            .GameProcess.Characters
                            .Where(x => x != character && x.Position.IsInside(character.Position, _ability.Radius))
                            .Select(x =>
                                new UseAbilityMove()
                                {
                                    Ability = _ability,
                                    Target = x,
                                });
                    }
                }
            }

            public void Dispose()
            {
                if (_ability is not null)
                    _ability.OnChange -= Update;
                
                character.HP.Current.Changed -= UpdateMagic;
            }
        }
        
        public AbilitiesComponent(GameProcessScreen screen, ICharacter character): base(screen.game)
        {
            this.screen = screen;
            this.character = character;
        }

        public override void Initialize()
        {
            base.Initialize();

            var box = new InvisiblePanel(Anchor.BottomRight, 0.7f, 0.1f);

            for (int i = 1; i < ABILITIES_COUNT; i++)
                boxes[i] = new AbilityBox(screen, box, i, character);
            boxes[0] = new AbilityBox(screen, box, 0, character);
            
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
            {
                foreach (var box in boxes)
                    box.Dispose();
                
                screen.game.UiSystem.Remove("abilities");
            }
        }
    }
}