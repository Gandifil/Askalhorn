using System;
using Askalhorn.Characters;
using Askalhorn.Combat;
using Askalhorn.Text;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Characters
{
    public class PrimaryAttributesViewer: FixPanel
    {
        public PrimaryAttributesViewer(ICharacter character, Anchor anchor, float width, float height): base(anchor, width, height)
        {
            AddChild(new TitleCustomText("Первичные характеристики"));
            AddChild(new VerticalSpace(10));
            foreach (var type in (PrimaryType[]) Enum.GetValues(typeof(PrimaryType)))
            {
                AddChild(new VerticalSpace(3));
                var textPointer = new EnumTextPointer<PrimaryType>(type);
                AddChild(new CustomText(Anchor.AutoLeft, $"{textPointer}:  {character.Primary[type].Value}"));
            }
        }
    }
}