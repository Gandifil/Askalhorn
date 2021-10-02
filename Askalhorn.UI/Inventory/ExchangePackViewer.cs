using System;
using Askalhorn.Inventory;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Inventory
{
    public class ExchangePackViewer: PackViewer
    {
        public ExchangePackViewer(Pack pack, Anchor anchor, float x, float y) : base(pack, anchor, x, y)
        {
        }

        protected override void SecondaryPressed(Element element)
        {
            Pressed?.Invoke(_pack);
        }

        public event Action<Pack> Pressed;
    }
}