using System;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended.Input.InputListeners;

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
                PutOn(_slot);
            _slot.OnPutOn += PutOn;
            _slot.OnTakeOff += TakeOff;
            
            CanBeMoused = true;

            OnMouseEnter += EnableSelecting;

            OnMouseExit += DisableSelecting;

            new Tooltip(100, slot.Type.ToString(), this);

            DragAndDrop.OnDrop += DropItem;
            InputListeners.Mouse.MouseDragStart += OnMouseDragStart;
        }

        private void OnMouseDragStart(object? sender, MouseEventArgs e)
        {
            if (DisplayArea.Contains(e.Position.ToVector2()))
            if (_slot.Item is not null)
            {
                var element = new DragAndDrop(_slot.Item);
                element.OnSuccesfullyDrop += () => { _slot.TakeOff();};
                element.Show(Root.System);
            }
        }

        public override void Dispose()
        {
            InputListeners.Mouse.MouseDragStart -= OnMouseDragStart;
            _slot.OnPutOn -= PutOn;
            _slot.OnTakeOff -= TakeOff;
            DragAndDrop.OnDrop -= DropItem;
            
            base.Dispose();
        }

        private void EnableSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>(Color.Gray);
        }

        private void DisableSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>();
        }

        private void PutOn(Slot slot)
        {
            var icon = new IconViewer(slot.Item, Anchor.Center, 1f, 1f);
            icon.OnMouseEnter += EnableSelecting;
            icon.OnMouseExit += DisableSelecting;
            
            AddChild(icon);
        }

        private void TakeOff(Slot slot)
        {
            RemoveChildren();
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
    }
}