using System;
using System.Net.Http.Headers;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public static class CharacterTab
    {
        private static readonly string NAME = "character";

        public static void Toggle(UiSystem system, ICharacter character)
        {
            if (system.Get(NAME) is null)
                system.Add(NAME, Create(character));
            else
                system.Remove(NAME);
        }
        
        public static void Open(UiSystem system, ICharacter character) 
        {
            if (system.Get(NAME) is null)
            {
                var box = Create(character);
                system.Add(NAME, box);
            }
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

            return box;
        }

        public static void Close(UiSystem system)
        {
            if (system.Get(NAME) is not null)
            {
                system.Remove(NAME);
            }
        }
    }
}