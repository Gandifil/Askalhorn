using System;
using System.Linq;
using Askalhorn.Common.Inventory;
using Askalhorn.Elements.Inventory.Search;
using MLEM.Ui;

namespace Askalhorn.Elements.Inventory
{
    public class BagViewer: InvisiblePanel
    {
        private readonly IBag _bag;
        private readonly FiltersBar _filtersBar;
        
        public BagViewer(IBag bag, FiltersBar filtersBar, Anchor anchor, float x, float y): 
            base(anchor, x, y, true)
        {
            _bag = bag;

            _bag.OnChanged += ResetPacks;

            DragAndDrop.OnDrop += DropItem;

            _filtersBar = filtersBar;
            _filtersBar.OnChanged += ResetPacks;

            ResetPacks();
        }

        private void DropItem(DragAndDrop element)
        {
            if (DisplayArea.Contains(element.PositionOffset))
            {
                try
                {
                    _bag.Put(element.Icon as IItem);
                    element.SuccesfullyDrop();
                }
                catch (ArgumentException e)
                {
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _bag.OnChanged -= ResetPacks;

            DragAndDrop.OnDrop -= DropItem;
            _filtersBar.OnChanged -= ResetPacks;
        }

        private void ResetPacks()
        {
            RemoveChildren();
            foreach (var pack in _bag.Items.Where(x => _filtersBar.Predicate(x.Item)))
            {
                AddChild(CreatePackViewer(pack));
            }
        }

        protected virtual PackViewer CreatePackViewer(Pack pack)
        {
            return new PackViewer(pack, Anchor.AutoCenter, 0.9f, 0.07f);
        }
    }
}