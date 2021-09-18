using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    public class FixPanel: Panel
    {
        private const int DEFAULT_CHILD_PADDING = 18;
        
        public FixPanel(Anchor anchor, float width, float height, bool scrollOverflow = false, int childPadding = DEFAULT_CHILD_PADDING) : 
            base(anchor, new Vector2(width, height), new Vector2(width, height), false, 
                scrollOverflow, new Point(15, 20), true)
        {
            ChildPadding = new Vector2(childPadding);
        }
    }
}