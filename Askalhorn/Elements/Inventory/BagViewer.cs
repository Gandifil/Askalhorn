using Askalhorn.Common.Inventory;
using MLEM.Ui;

namespace Askalhorn.Elements.Inventory
{
    public class BagViewer: InvisiblePanel
    {
        private readonly IBag _bag;
        
        public BagViewer(IBag bag, Anchor anchor, float x, float y): 
            base(anchor, x, y, true)
        {
            _bag = bag;

            _bag.OnChanged += ResetPacks;

            ResetPacks();
        }

        public override void Dispose()
        {
            base.Dispose();

            _bag.OnChanged -= ResetPacks;
        }

        private void ResetPacks()
        {
            RemoveChildren();
            foreach (var pack in _bag.Items)
            {
                AddChild(CreatePackViewer(pack));
            }
        }

        protected virtual PackViewer CreatePackViewer(Pack pack)
        {
            return new PackViewer(pack, Anchor.AutoCenter, 0.9f, 0.05f);
        }
    }
}