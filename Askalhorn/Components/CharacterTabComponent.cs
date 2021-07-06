using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
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
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player));
        }
        
        private static Element Create(ICharacter character)
        {
            var box = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.9f), Vector2.Zero);
            box.AddChild(BaseBox(character));
            box.AddChild(PrimaryBox(character));
            box.AddChild(SecondaryBox(character));
            return box;
        }
        private static Panel BaseBox(ICharacter character)
        {
            var box = new Panel(Anchor.TopLeft, new Vector2(0.5f, 0.3f), Vector2.Zero);
            box.AddChild(new Image(Anchor.AutoCenter, new Vector2(0.9f, 0.9f), new TextureRegion(character.Texture)));
            box.AddChild(new Paragraph(Anchor.AutoCenter, 100, character.Name));
            return box;
        }

        private static Panel PrimaryBox(ICharacter character)
        {
            var box = new Panel(Anchor.TopRight, new Vector2(0.5f, 0.3f), Vector2.Zero);
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
            var box = new Panel(Anchor.BottomCenter, new Vector2(1.0f, 0.7f), Vector2.Zero);
            //box.AddChild(new Paragraph(Anchor.AutoCenter, 100, "Эффекты:\t"));
            foreach (var type in (SecondaryTypes[]) Enum.GetValues(typeof(SecondaryTypes)))
            {
                box.AddChild(new VerticalSpace(3));
                box.AddChild(new Paragraph(Anchor.AutoLeft, 1, type+":\t" + character.Secondary[type]));
            }
            return box;
        }


        private static Panel EffectsBox(ICharacter character)
        {
            var box = new Panel(Anchor.BottomCenter, new Vector2(0.9f, 0.4f), Vector2.Zero);
            box.AddChild(new Paragraph(Anchor.AutoCenter, 100, "Эффекты:\t"));
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