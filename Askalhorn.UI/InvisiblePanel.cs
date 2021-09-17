using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    public class InvisiblePanel: Panel
    {
        public InvisiblePanel(Anchor anchor, float x, float y, bool scrollOverflow = false) : 
            base(anchor, new Vector2(x, y), Vector2.Zero, false, scrollOverflow, null, true)
        {
            ChildPadding = new Vector2(0);
            Texture = null;
        }
    }
}