using System;
using Askalhorn.Characters;
using Askalhorn.Combat;
using Askalhorn.Text;
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
            box.AddChild(new CustomText(Anchor.AutoCenter, character.Name));
            box.AddChild(new CustomText(Anchor.AutoCenter, "Уровень: \t"+ character.Level));
            return box;
        }

        private static Panel PrimaryBox(ICharacter character)
        {
            var box = new FixPanel(Anchor.TopRight, 0.5f, 0.3f);
            box.AddChild(new CustomText(Anchor.AutoLeft, "Первичные характеристики"));
            foreach (var type in (PrimaryType[]) Enum.GetValues(typeof(PrimaryType)))
            {
                box.AddChild(new VerticalSpace(3));
                var textPointer = new EnumTextPointer<PrimaryType>(type);
                box.AddChild(new CustomText(Anchor.AutoLeft, $"{textPointer}:  {character.Primary[type].Value}"));
            }
            return box;
        }
        
        private static Panel SecondaryBox(ICharacter character)
        {
            var box = new FixPanel(Anchor.BottomCenter, 1.0f, 0.7f);
            box.AddChild(new CustomText(Anchor.AutoLeft, "Вторичные характеристики"));
            foreach (var type in (SecondaryType[]) Enum.GetValues(typeof(SecondaryType)))
            {
                box.AddChild(new VerticalSpace(3));
                var textPointer = new EnumTextPointer<SecondaryType>(type);
                box.AddChild(new CustomText(Anchor.AutoLeft, $"{textPointer}:  {character.Secondary[type].Value}"));
            }
            
            box.AddChild(new CustomText(Anchor.AutoLeft, "Защита"));
            foreach (var type in (DamageType[]) Enum.GetValues(typeof(DamageType)))
            {
                box.AddChild(new VerticalSpace(3));
                var textPointer = new EnumTextPointer<DamageType>(type)
                {
                    GrammaticalCase = GrammaticalCase.Genitive,
                };
                box.AddChild(new CustomText(Anchor.AutoLeft, $"Защита от {textPointer}:  {character.Protection[type].Value}"));
            }
            return box;
        }
        
    }
}