using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public class MenuIB: InvisiblePanel, IDisposable
    {
        private readonly Panel _panel;
        private readonly Texture2D _background;
        private readonly UiSystem _uiSystem;

        public const int ELEMENT_HEIGHT = 40;
        public const int VERTICAL_SPACE_HEIGHT = 5;

        public MenuIB(Anchor anchor, float x, float y) : base(anchor, x, y, true)
        {
            //SetHeightBasedOnChildren = true;
        }

        public void AddButton(string label, Action pressAction)
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentNullException("Argument 'label' can't be null or empty");
            
            if (pressAction is null)
                throw new ArgumentNullException("Argument 'pressAction' can't be null");
            
            Add(new Button(Anchor.AutoCenter, new Vector2(1, ELEMENT_HEIGHT), label)
            {
                OnPressed = _ => pressAction(),
            });
        }

        public void Clear()
        {
            isEmptyPanel = true;
            RemoveChildren();
        }

        public ScrollBar AddScroll(string label, float max = 1.0f)
        {
            var scrollBar = new ScrollBar(Anchor.AutoCenter, new Vector2(0.8f, ELEMENT_HEIGHT), 
                ELEMENT_HEIGHT, max, true);
            Add(new Paragraph(Anchor.AutoCenter, 300, label));
            Add(scrollBar, false);
            return scrollBar;
        }

        private bool isEmptyPanel = true;

        public void Add(Element element, bool spacing = true)
        {
            if (isEmptyPanel)
                isEmptyPanel = false;
            else
                if (spacing)
                    AddChild(new VerticalSpace(VERTICAL_SPACE_HEIGHT));
            
            AddChild(element);
        }
    }
}