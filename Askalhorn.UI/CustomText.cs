using Askalhorn.Common;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Content;

namespace Askalhorn.UI
{
    public class CustomText: Paragraph
    {
        public CustomText(Anchor anchor, string text): 
            base(anchor, 1, text, true)
        {
            TextColor = Color.Black;
        }
        
        public CustomText(Anchor anchor, float width, string text): 
            base(anchor, Storage.Content.GetGraphicsDevice().Viewport.Width * width, text)
        {
            TextColor = Color.Black;
        }
        
        public CustomText(Anchor anchor, uint width, string text): 
            base(anchor, width, text)
        {
            TextColor = Color.Black;
        }
    }

    public class TitleCustomText : CustomText
    {
        public TitleCustomText(string text, Anchor anchor = Anchor.AutoCenter) : base(anchor, text)
        {
            
        }
    }
}