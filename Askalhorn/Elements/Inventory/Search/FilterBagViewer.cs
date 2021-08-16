using Askalhorn.Common.Inventory;
using MLEM.Ui;

namespace Askalhorn.Elements.Inventory.Search
{
    public class FilterBagViewer: InvisiblePanel
    {
        private readonly IBag _bag;
        
        public FilterBagViewer(IBag bag, Anchor anchor, float x, float y) : base(anchor, x, y)
        {
            _bag = bag;
            var filters = new FiltersBar(Anchor.AutoCenter, 1, 0.1f);
            AddChild(filters);
            AddChild(new BagViewer(_bag, filters, Anchor.AutoCenter, 1, 0.9f));
        }
    }
}