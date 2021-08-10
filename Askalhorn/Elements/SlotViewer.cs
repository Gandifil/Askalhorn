using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.Elements
{
    public class SlotViewer: FixPanel
    {
        public SlotViewer(Slot slot, Anchor anchor, float width, float height): 
            base(anchor, width, height)
        {
            CanBeMoused = true;

            OnMouseEnter = EnableSelecting;

            OnMouseExit = DisableSelecting;

            new Tooltip(100, slot.Type.ToString(), this);
        }

        private void EnableSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>(Color.Gray);
        }

        private void DisableSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>();
        }
    }
}