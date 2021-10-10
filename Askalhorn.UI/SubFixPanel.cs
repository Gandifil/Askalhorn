using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    public class SubFixPanel: Panel
    {
        private const int DEFAULT_CHILD_PADDING = 5;
        
        public SubFixPanel(Anchor anchor, float width, float height, bool scrollOverflow = false, int childPadding = DEFAULT_CHILD_PADDING) : 
            base(anchor, new Vector2(width, height), new Vector2(width, height), false, 
                scrollOverflow, new Point(15, 20), true)
        {
            ChildPadding = new Vector2(childPadding);
            var testTexture = Storage.Content.Load<Texture2D>("images/Test");
            Texture = new NinePatch(new TextureRegion(testTexture, 0, 8, 24, 24), 3);
        }
    }
}