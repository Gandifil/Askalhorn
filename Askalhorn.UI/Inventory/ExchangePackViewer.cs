using Askalhorn.Inventory;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Inventory
{
    public class ExchangePackViewer: PackViewer
    {
        private readonly Bag _target;
        
        public ExchangePackViewer(Bag target, Pack pack, Anchor anchor, float x, float y) : base(pack, anchor, x, y)
        {
            _target = target;
        }

        protected override void DoubleClick(Element element)
        {
            _target.Put(_pack.Item);
            _pack.Remove();
        }
    }
}