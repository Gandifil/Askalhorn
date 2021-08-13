using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public class FixPanel: Panel
    {
        private const int CHILD_PADDING = 10;
        
        public FixPanel(Anchor anchor, float width, float height, bool scrollOverflow = false) : 
            base(anchor, new Vector2(width, height), new Vector2(width, height), false, 
                scrollOverflow, new Point(15, 20), true)
        {
            ChildPadding = new Vector2(CHILD_PADDING);
        }
    }
}