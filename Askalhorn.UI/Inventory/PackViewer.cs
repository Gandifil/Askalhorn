﻿using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Askalhorn.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

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
            //CanBeSelected = true;
            OnMouseEnter += SetSelecting;
            OnMouseExit += SetUnselecting;
            OnPressed += CreateDragAndDrop;
            OnSecondaryPressed += DoubleClick;

            var icon = new IconViewer(_pack.Item, Anchor.CenterLeft, 0.1f, 1f);
            icon.OnMouseEnter += SetSelecting;
            icon.OnMouseExit += SetUnselecting;
            icon.OnPressed += CreateDragAndDrop;
            icon.OnSecondaryPressed += DoubleClick;
            
            AddChild(icon);
            AddChild(new Paragraph(Anchor.Center, 300, _pack.Item.Name));
            _count = new Paragraph(Anchor.CenterRight, 150, "x" + _pack.Count);
            AddChild(_count);
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

        private void CreateDragAndDrop(Element target)
        {
            var element = new DragAndDrop(_pack.Item);
            element.OnSuccesfullyDrop += () => { _pack.Remove();}; // TODO: add removing item from player's bag;
            element.Show(Root.System);
        }

        private void ResetCount()
        {
            _count.Text = "x" + _pack.Count;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _pack.OnCountChange -= ResetCount;
        }
    }
}