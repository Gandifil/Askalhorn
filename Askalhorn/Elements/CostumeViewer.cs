using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public class CostumeViewer: InvisiblePanel
    {
        private const float SLOT_VIEWER_WIDTH = 0.14f;
        private const float SLOT_VIEWER_HEIGHT = 0.14f;
        private const int BERTICAL_SPACE_SIZE = 16;

        private float GetYCoordinate(int index)
        {
            float b = Area.Height / 100;

            return b * (5 + index * (5 + 14));
        }
        
        public CostumeViewer(Costume costume, Anchor anchor, float x, float y) : 
            base(anchor, x, y, false)
        {
            var left = new InvisiblePanel(Anchor.CenterLeft, SLOT_VIEWER_WIDTH, 1f);
            for (IItem.PurposeType i = IItem.PurposeType.Head; i <= IItem.PurposeType.Boots; i++)
            {
                left.AddChild(new SlotViewer(costume.Clothes[i], Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
                left.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            }
            AddChild(left);
            
            var right = new InvisiblePanel(Anchor.CenterRight, SLOT_VIEWER_WIDTH, 1f);
            for (IItem.PurposeType i = IItem.PurposeType.Amulet; i <= IItem.PurposeType.Ring; i++)
            {
                right.AddChild(new SlotViewer(costume.Clothes[i], Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
                right.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            }
            right.AddChild(new SlotViewer(costume.SecondRing, Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
            right.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            right.AddChild(new SlotViewer(costume.Clothes[IItem.PurposeType.Shield], Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
            right.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            AddChild(right);
            AddChild(new SlotViewer(costume.Clothes[IItem.PurposeType.Weapon], Anchor.BottomCenter, SLOT_VIEWER_WIDTH, SLOT_VIEWER_HEIGHT));
        }
    }
}