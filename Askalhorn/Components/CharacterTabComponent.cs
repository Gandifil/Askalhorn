using System;
using AmbrosiaGame.Screens;
using Askalhorn.Characters;
using Askalhorn.Common;
using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class CharacterTabComponent: IGameComponent, IDisposable
    {
        private static readonly string NAME = "character";
        private readonly GameProcessScreen screen;

        public CharacterTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            screen.game.UiSystem.Add(NAME, Create(screen.GameProcess.Player));
        }
        
        private static Element Create(ICharacter character)
        {
            var box = new FixPanel(Anchor.CenterRight, 0.45f, 0.9f);
            box.AddChild(BaseBox(character));
            box.AddChild(PrimaryBox(character));
            box.AddChild(SecondaryBox(character));
            return box;
        }
        private static Panel BaseBox(ICharacter character)
        {
            var box = new FixPanel(Anchor.TopLeft, 0.5f, 0.3f);
            //box.AddChild(new Image(Anchor.AutoCenter, new Vector2(0.9f, 0.9f), character.Rend));
            box.AddChild(new Paragraph(Anchor.AutoCenter, 100, character.Name));
            return box;
        }

        private static Panel PrimaryBox(ICharacter character)
        {
            var box = new FixPanel(Anchor.TopRight, 0.5f, 0.3f);
            box.AddChild(new Paragraph(Anchor.AutoCenter, 1, "Уровень: \t"+ character.Level));
            foreach (var type in (PrimaryTypes[]) Enum.GetValues(typeof(PrimaryTypes)))
            {
                box.AddChild(new VerticalSpace(3));
                box.AddChild(new Paragraph(Anchor.AutoLeft, 1, type+":\t" + character.Primary[type]));
            }
            return box;
        }
        private static Panel SecondaryBox(ICharacter character)
        {
            var box = new FixPanel(Anchor.BottomCenter, 1.0f, 0.7f);
            foreach (var type in (SecondaryTypes[]) Enum.GetValues(typeof(SecondaryTypes)))
            {
                box.AddChild(new VerticalSpace(3));
                box.AddChild(new Paragraph(Anchor.AutoLeft, 1, type+":\t" + character.Secondary[type]));
            }
            foreach (var type in (DamageTypes[]) Enum.GetValues(typeof(DamageTypes)))
            {
                box.AddChild(new VerticalSpace(3));
                box.AddChild(new Paragraph(Anchor.AutoLeft, 1, type+":\t" + character.Protection[type]));
            }
            return box;
        }
        
        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
    }
}