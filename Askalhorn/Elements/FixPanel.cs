using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public class FixPanel: Panel
    {
        public FixPanel(Anchor anchor, float x, float y, bool scrollOverflow = false) : 
            base(anchor, new Vector2(x, y), Vector2.Zero, false, scrollOverflow, new Point(15, 20), true)
        {
            ChildPadding = new Vector2(16);
        }
    }
}