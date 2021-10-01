using Askalhorn.Inventory;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Inventory
{
    public class ExchangeWindow:FixPanel
    {
        public ExchangeWindow(Bag left, Bag right, Anchor anchor = Anchor.Center, float width =.9f, float height =.9f): 
            base(anchor, width, height)
        {
            InputListeners.Input.MouseListener.Push(new MouseListener());
            AddChild(new ExchangeBagViewer("Контейнер", right, left, Anchor.CenterLeft, 0.45f, 0.9f));
            AddChild(new ExchangeBagViewer("Инвентарь", left, right, Anchor.CenterRight, 0.45f, 0.9f));
            AddChild(new Button(Anchor.BottomLeft, new Vector2(0.45f, 0.04f), "Взять все")
            {
                OnPressed = _ =>
                {
                    left.MergeTo(right);
                    this.Close();
                },
            });
            AddChild(new Button(Anchor.BottomRight, new Vector2(0.45f, 0.04f), "Выйти")
            {
                OnPressed = _ => this.Close(),
            });
        }

        public override void Dispose()
        {
            InputListeners.Input.MouseListener.Pop();
            
            base.Dispose();
        }
    }
}