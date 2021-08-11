using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Inventory;
using Askalhorn.Components;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements.Icons
{
    public class ItemIconElement: IconElement
    {
        private readonly IBag.Pack _pack;
        
        public ItemIconElement(IBag.Pack pack, Panel parent, Anchor anchor, Vector2 position):
            base(pack.Item, parent, anchor, position)
        {
            _pack = pack;
        }

        protected override Element BuildRootElement()
        {
            var box = new Panel(_anchor, _position, Vector2.Zero);
            box.CanBeMoused = true;
            box.CanBeSelected = true;
            box.OnMouseEnter += element => element.Size = new Vector2(0.95f, 0.07f);
            box.OnMouseExit += element => element.Size = new Vector2(0.9f, 0.05f);
            AddTooltipTo(box);
            //box.OnPressed += _ => World.Instance.playerController.AddMove(new UseItemMove(_pack.Item));
            box.OnPressed += CreateDragAndDrop;//Components.Add(new DragAndDropComponent(_pack.Item));
            
            box.AddChild(CreateTextureImage(Anchor.CenterLeft, new Vector2(0.2F, 1F)));
            box.AddChild(new Paragraph(Anchor.Center, 300, _pack.Item.Name));
            box.AddChild(new Paragraph(Anchor.CenterRight, 150, "x" + _pack.Count.ToString()));
            
            return box;
        }

        private void CreateDragAndDrop(Element target)
        {
            var element = new DragAndDrop(_icon);
            element.OnSuccesfullyDrop += () => {}; // TODO: add removing item from player's bag;
            element.Show();
        }
    }
}