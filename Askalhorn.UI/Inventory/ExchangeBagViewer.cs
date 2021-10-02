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
        }

        protected override PackViewer CreatePackViewer(Pack pack)
        {
            var viewer = new ExchangePackViewer(pack, Anchor.AutoCenter, 1f, 0.1f);
            viewer.Pressed += ViewerOnPressed;
            return viewer;
        }

        private void ViewerOnPressed(Pack _pack)
        {
            _target.Put(_pack);
            _pack.Remove(_pack.Count);
        }
    }
}