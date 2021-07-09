using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Misc;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.Components
{
    public class AbilitiesTabComponent: IGameComponent, IDisposable
    {
        private static readonly string NAME = "abilitiesTab";
        private readonly GameProcessScreen screen;

        public AbilitiesTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player));
        }
        
        private static Element Create(ICharacter character)
        {
            var box = new Panel(Anchor.CenterLeft, new Vector2(0.45f, 0.9f), Vector2.Zero);
            foreach (var ability in character.Abilities)
                box.AddChild(CreateItem(ability));
            return box;
        }

        private static Element CreateItem(IAbility ability)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.1f), Vector2.Zero);
            var icon = new Image(Anchor.CenterLeft, new Vector2(-1, 1F), ability.Icon.ToMlem());
            icon.CanBeMoused = true;
            var tt = new Tooltip(500, ability.ToString(), icon);
            tt.MouseOffset = new Vector2(32, -64);
            box.AddChild(icon);
            box.AddChild(new Paragraph(Anchor.TopRight, 500, ability.Name, true));
            box.AddChild(new ProgressBar(Anchor.BottomRight, new Vector2(0.8f, 0.5f), Direction2.Right, ability.MaxSkill, ability.Skill));
            var modBox = new Panel(Anchor.TopCenter, new Vector2(0.3f, 0.5f), Vector2.Zero);
            modBox.Texture = null;
            int i = 0;
            foreach (var modification in ability.Modifications)
            {
                var image = new Image(Anchor.AutoInlineIgnoreOverflow, new Vector2(-1, 1F), modification.Icon.ToMlem())
                {
                    Padding = new Padding(3, 3, 3, 3),
                };
                
                if (ability.CurrentModification != -1 && ability.CurrentModification != i)
                    image.Color = new StyleProp<Color>(Color.Gray);
                image.CanBeMoused = true;
                int x = i;
                image.OnPressed += element => ability.CurrentModification = x;
                var tooltip = new Tooltip(500, modification.Description, image);
                tooltip.MouseOffset = new Vector2(32, -64);
                modBox.AddChild(image);
                i++;
            }
            box.AddChild(modBox);
            return box;
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
        
    }
}