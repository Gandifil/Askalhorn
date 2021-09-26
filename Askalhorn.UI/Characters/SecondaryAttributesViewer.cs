using System;
using Askalhorn.Characters;
using Askalhorn.Combat;
using Askalhorn.Text;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Characters
{
    public class SecondaryAttributesViewer: FixPanel
    {
        public SecondaryAttributesViewer(ICharacter character, Anchor anchor, float width, float height): base(anchor, width, height)
        {
            AddChild(new TitleCustomText("Вторичные характеристики"));
            AddChild(new VerticalSpace(10));
            foreach (var type in (SecondaryType[]) Enum.GetValues(typeof(SecondaryType)))
            {
                AddChild(new VerticalSpace(3));
                var textPointer = new EnumTextPointer<SecondaryType>(type);
                AddChild(new CustomText(Anchor.AutoLeft, $"{textPointer}:  {character.Secondary[type].Value}"));
            }
        }
    }
}