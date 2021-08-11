using System;
using System.Xml.Linq;
using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.Elements
{
    public class SlotViewer: FixPanel
    {
        private readonly Slot _slot;
        
        public SlotViewer(Slot slot, Anchor anchor, float width, float height): 
            base(anchor, width, height)
        {
            _slot = slot;
            
            CanBeMoused = true;

            OnMouseEnter = EnableSelecting;

            OnMouseExit = DisableSelecting;

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

        private void DropItem(DragAndDrop element)
        {
            if (DisplayArea.Contains(element.PositionOffset))
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
            
            DragAndDrop.OnDrop -= DropItem;
        }
    }
}