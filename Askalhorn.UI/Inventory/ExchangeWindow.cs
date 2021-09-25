using Askalhorn.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Inventory
{
    public class ExchangeWindow:FixPanel
    {
        public ExchangeWindow(Bag left, Bag right, Anchor anchor = Anchor.Center, float width =.9f, float height =.9f): 
            base(anchor, width, height)
        {
            AddChild(new ExchangeBagViewer(right, left, Anchor.CenterLeft, 0.45f, 0.9f));
            AddChild(new ExchangeBagViewer(left, right, Anchor.CenterRight, 0.45f, 0.9f));
            AddChild(new Button(Anchor.BottomRight, new Vector2(0.45f, 0.04f), "Выйти")
            {
                OnPressed = _ => Dispose(),
            });
        }
    }
}