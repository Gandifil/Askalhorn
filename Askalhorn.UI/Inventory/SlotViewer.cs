using System;
using Askalhorn.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.UI.Inventory
{
    public class SlotViewer: FixPanel
    {
        private readonly Slot _slot;
        
        public SlotViewer(Slot slot, Anchor anchor, float width, float height): 
            base(anchor, width, height)
        {
            _slot = slot;
            if (_slot.Item is not null)
                PutOn(_slot.Item);
            _slot.OnPutOn += PutOn;
            _slot.OnTakeOff += TakeOff;
            
            CanBeMoused = true;

            OnMouseEnter += EnableSelecting;

            OnMouseExit += DisableSelecting;
            
            OnPressed += Pressed;

            DragAndDrop.OnDrop += DropItem;

            new Tooltip(100, slot.Type.ToString(), this);
        }

        private void EnableSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>(Color.Gray);
        }

        private void DisableSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>();
        }

        private void PutOn(IItem item)
        {
            var icon = new IconViewer(item, Anchor.Center, 1f, 1f);
            icon.OnMouseEnter += EnableSelecting;
            icon.OnMouseExit += DisableSelecting;
            icon.OnPressed += Pressed;
            
            AddChild(icon);
        }

        private void TakeOff(IItem item)
        {
            RemoveChildren();
        }

        private void Pressed(Element e)
        {
            if (_slot.Item is not null)
            {
                var element = new DragAndDrop(_slot.Item);
                element.OnSuccesfullyDrop += () => { _slot.TakeOff();};
                element.Show(Root.System);
            }
        }

        private void DropItem(DragAndDrop element)
        {
            if (DisplayArea.Contains(element.PositionOffset) && element.Icon  != _slot.Item)
            {
                try
                {
                    _slot.PutOn(element.Icon as IItem);
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
            
            _slot.OnPutOn -= PutOn;
            _slot.OnTakeOff -= TakeOff;
            DragAndDrop.OnDrop -= DropItem;
        }
    }
}