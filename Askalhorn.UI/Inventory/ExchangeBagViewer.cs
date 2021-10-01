using Askalhorn.Inventory;
using MLEM.Ui;

namespace Askalhorn.UI.Inventory
{
    public class ExchangeBagViewer: BagViewer
    {
        private readonly Bag _target;

        public ExchangeBagViewer(string title, Bag target, Bag bag, Anchor anchor, float x, float y) : base(bag, anchor, x, y)
        {
            _target = target;
            //AddChild(new TitleCustomText(title, Anchor.TopCenter));
        }

        protected override PackViewer CreatePackViewer(Pack pack)
        {
            return new ExchangePackViewer(_target, pack, Anchor.AutoCenter, 1f, 0.1f);
        }
    }
}