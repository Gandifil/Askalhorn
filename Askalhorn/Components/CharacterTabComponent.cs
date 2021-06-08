using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
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
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player));
        }
        
        private static Element Create(ICharacter character)
        {
            var box = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.9f), Vector2.Zero);
            box.AddChild(new Paragraph(Anchor.AutoLeft, 1, "Имя:\t"+ character.Name));
            box.AddChild(new Paragraph(Anchor.AutoLeft, 1, "Уровень:\t"+ character.Level.ToString()));
            box.AddChild(new VerticalSpace(15));
            box.AddChild(new Paragraph(Anchor.AutoLeft, 1, "Основные параметры:\t"));
            foreach (var type in (PrimaryTypes[]) Enum.GetValues(typeof(PrimaryTypes)))
            {
                box.AddChild(new VerticalSpace(3));
                box.AddChild(new Paragraph(Anchor.AutoLeft, 1, type.ToString()+":\t" + character.Primary[type].ToString()));
            }
            box.AddChild(new VerticalSpace(15));
            box.AddChild(new Paragraph(Anchor.AutoLeft, 1, "Эффекты:\t"));

            foreach (var effect in character.Effects)
                box.AddChild(CreateItem(effect));
            return box;
        }

        private static Element CreateItem(IEffect effect)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.05f), Vector2.Zero);
            box.AddChild(new Image(Anchor.CenterLeft, new Vector2(0.2F, 1F), effect.TextureRegion.ToMlem()));
            box.AddChild(new Paragraph(Anchor.Center, 600, effect.Description));
            box.AddChild(new Paragraph(Anchor.CenterRight, 100, effect.TurnCount.ToString()));
            return box;
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
    }
}