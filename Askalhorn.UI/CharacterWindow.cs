using System;
using Askalhorn.Characters;
using Askalhorn.Combat;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    public class CharacterWindow: FixPanel
    {
        public CharacterWindow(ICharacter character, Anchor anchor = Anchor.CenterRight, float width = .45f, float height = .9f): 
            base(anchor, width, height)
        {
            AddChild(BaseBox(character));
            AddChild(PrimaryBox(character));
            AddChild(SecondaryBox(character));
        }
        
        private static Panel BaseBox(ICharacter character)
        {
            var box = new FixPanel(Anchor.TopLeft, 0.5f, 0.3f);
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
    }
}