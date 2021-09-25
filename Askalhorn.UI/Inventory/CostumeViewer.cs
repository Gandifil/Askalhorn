using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Inventory
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
            for (ItemPurpose i = ItemPurpose.Head; i <= ItemPurpose.Boots; i++)
            {
                left.AddChild(new SlotViewer(costume.Clothes[i], Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
                left.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            }
            AddChild(left);
            
            var right = new InvisiblePanel(Anchor.CenterRight, SLOT_VIEWER_WIDTH, 1f);
            for (ItemPurpose i = ItemPurpose.Amulet; i <= ItemPurpose.Ring; i++)
            {
                right.AddChild(new SlotViewer(costume.Clothes[i], Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
                right.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            }
            right.AddChild(new SlotViewer(costume.SecondRing, Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
            right.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            right.AddChild(new SlotViewer(costume.Clothes[ItemPurpose.Shield], Anchor.AutoCenter, 1f, SLOT_VIEWER_HEIGHT));
            right.AddChild(new VerticalSpace(BERTICAL_SPACE_SIZE));
            AddChild(right);
            AddChild(new SlotViewer(costume.Clothes[ItemPurpose.Weapon], Anchor.BottomCenter, SLOT_VIEWER_WIDTH, SLOT_VIEWER_HEIGHT));
        }
    }
}