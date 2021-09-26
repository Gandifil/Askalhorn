using Askalhorn.Characters;
using Askalhorn.Render;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Characters
{
    public class CharacterViewer: FixPanel
    {
        public CharacterViewer(ICharacter character, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new TitleCustomText(character.Name));
            AddChild(new VerticalSpace(10));
            AddChild(new CustomText(Anchor.AutoCenter, "Уровень: \t"+ character.Level));
            var textureRenderer = character.Renderer as TextureRenderer;
            if (textureRenderer is not null)
            {
                AddChild(new Image(Anchor.AutoCenter, new Vector2(.7f, -1f), textureRenderer.Region.ToMlem()));
            }
            AddChild(new CustomText(Anchor.AutoCenter, $"HP: {character.HP.Current}/{character.HP.Max}"));
            AddChild(new CustomText(Anchor.AutoCenter, $"MP: {character.MP.Current}/{character.MP.Max}"));
            
        }
    }
}