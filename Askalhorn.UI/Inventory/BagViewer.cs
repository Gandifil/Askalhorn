﻿using System;
using System.Linq;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.UI.Input;
using Askalhorn.UI.Inventory.Search;
using MLEM.Ui;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Inventory
{
    public class BagViewer: InvisiblePanel
    {
        private readonly Bag _bag;
        private readonly FiltersBar _filtersBar;
        private readonly InvisiblePanel _packsPanel;
        
        public BagViewer(Bag bag, Anchor anchor, float x, float y): 
            base(anchor, x, y)
        {
            _bag = bag;
            InputListeners.Input.MouseListener.Push(new MouseListener());
            
            _filtersBar = new FiltersBar(Anchor.AutoCenter, 1, 0.1f);
            _filtersBar.OnChanged += ResetPacks;
            AddChild(_filtersBar);

            _packsPanel = new InvisiblePanel(Anchor.AutoCenter, 1, 0.9f, true);
            AddChild(_packsPanel);

            _bag.OnChanged += ResetPacks;

            DragAndDrop.OnDrop += DropItem;

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
            InputListeners.Input.MouseListener.Pop();
            
            base.Dispose();

            _bag.OnChanged -= ResetPacks;

            DragAndDrop.OnDrop -= DropItem;
            _filtersBar.OnChanged -= ResetPacks;
        }

        private void ResetPacks()
        {
            //_packsPanel.RecursiveDisposeChildren();
            _packsPanel.RemoveChildren();
            foreach (var pack in _bag.Items.Where(x => _filtersBar.Predicate(x.Item)))
            {
                _packsPanel.AddChild(CreatePackViewer(pack));
            }
        }

        protected virtual PackViewer CreatePackViewer(Pack pack)
        {
            return new PackViewer(pack, Anchor.AutoCenter, 0.9f, 0.07f);
        }
    }
}