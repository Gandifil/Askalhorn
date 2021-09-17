using Askalhorn.Inventory;
using MLEM.Ui;

namespace Askalhorn.UI.Inventory
{
    public class ExchangeBagViewer: BagViewer
    {
        private readonly IBag _target;

        public ExchangeBagViewer(IBag target, IBag bag, Anchor anchor, float x, float y) : base(bag, anchor, x, y)
        {
            _target = target;
        }

        protected override PackViewer CreatePackViewer(Pack pack)
        {
            return new ExchangePackViewer(_target, pack, Anchor.AutoCenter, 0.9f, 0.05f);
        }
    }
}