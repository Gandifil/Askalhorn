using System;
using Askalhorn.Characters;
using Askalhorn.Combat;
using Askalhorn.Text;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Characters
{
    public class ProtectionViewer: FixPanel
    {
        public ProtectionViewer(ICharacter character, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new TitleCustomText("Защита"));
            AddChild(new VerticalSpace(10));
            foreach (var type in (DamageType[]) Enum.GetValues(typeof(DamageType)))
            {
                AddChild(new VerticalSpace(3));
                var textPointer = new EnumTextPointer<DamageType>(type)
                {
                    GrammaticalCase = GrammaticalCase.Genitive,
                };
                AddChild(new CustomText(Anchor.AutoLeft, $"Защита от {textPointer}:  {character.Protection[type].Value}"));
            }
        }
    }
}