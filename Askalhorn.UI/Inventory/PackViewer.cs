using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Askalhorn.Inventory;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Inventory
{
    public class PackViewer: FixPanel
    {
        protected readonly Pack _pack;
        private readonly Paragraph _count;
        
        public PackViewer(Pack pack, Anchor anchor, float x, float y) : base(anchor, x, y)
        {
            _pack = pack;
            _pack.OnCountChange += ResetCount;

            SetUnselecting(this);
            CanBeMoused = true;
            OnMouseEnter += SetSelecting;
            OnMouseExit += SetUnselecting;
            OnSecondaryPressed += DoubleClick;

            var icon = new IconViewer(_pack.Item, Anchor.CenterLeft, 0.1f, 1f);
            icon.OnMouseEnter += SetSelecting;
            icon.OnMouseExit += SetUnselecting;
            icon.OnSecondaryPressed += DoubleClick;
            
            AddChild(icon);
            AddChild(new CustomText(Anchor.Center, _pack.Item.Name));
            _count = new CustomText(Anchor.CenterRight, "x" + _pack.Count);
            AddChild(_count);
            InputListeners.Mouse.MouseDragStart += OnMouseDragStart;
        }

        public override void Dispose()
        {
            InputListeners.Mouse.MouseDragStart -= OnMouseDragStart;
            _pack.OnCountChange -= ResetCount;
            
            base.Dispose();
        }

        private void OnMouseDragStart(object? sender, MouseEventArgs e)
        {
            if (DisplayArea.Contains(e.Position.ToVector2()))
            {
                var element = new DragAndDrop(_pack.Item);
                element.OnSuccesfullyDrop += () => { _pack.Remove();};
                element.Show(Root.System);
            }
        }

        protected virtual void DoubleClick(Element element)
        {
            GameProcess.Instance.Player.Make(new UseItemMove(_pack.Item));
        }

        private void SetSelecting(Element element)
        {
            DrawColor = new StyleProp<Color>(Color.Gray);
        }

        private void SetUnselecting(Element element)
        {
            DrawColor = new StyleProp<Color>(Color.LightGray);
        }

        private void ResetCount()
        {
            _count.Text = "x" + _pack.Count;
        }
    }
}