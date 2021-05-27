using System;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public static class CharacterTab
    {
        public static void Open(UiSystem system, ICharacter character) 
        {
            if (system.Get("inventory") is null)
            {
                var box = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.9f), Vector2.Zero);
                box.AddChild(new Paragraph(Anchor.AutoLeft, 1, "Уровень:\t"+ character.Level.ToString()));
                foreach (var type in (PrimaryTypes[]) Enum.GetValues(typeof(PrimaryTypes)))
                {
                    box.AddChild(new VerticalSpace(3));
                    box.AddChild(new Paragraph(Anchor.AutoLeft, 1, type.ToString()+":\t" + character.Primary[type].ToString()));
                }
                system.Add("inventory", box);
            }
        }

        public static void Close(UiSystem system)
        {
            if (system.Get("inventory") is not null)
            {
                system.Remove("inventory");
            }
        }
    }
}