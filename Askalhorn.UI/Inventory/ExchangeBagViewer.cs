using Askalhorn.Inventory;
using MLEM.Ui;

namespace Askalhorn.UI.Inventory
{
    public class ExchangeBagViewer: BagViewer
    {
        private readonly Bag _target;

        public ExchangeBagViewer(Bag target, Bag bag, Anchor anchor, float x, float y) : base(bag, anchor, x, y)
        {
            _target = target;
        }

        protected override PackViewer CreatePackViewer(Pack pack)
        {
            return new ExchangePackViewer(_target, pack, Anchor.AutoCenter, 0.9f, 0.05f);
        }
    }
}