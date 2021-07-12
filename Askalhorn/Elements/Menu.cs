using System;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MLEM.Misc;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public class Menu: IDisposable
    {
        private readonly Panel _panel;
        private readonly Texture2D _background;
        private readonly UiSystem _uiSystem;

        public const int ELEMENT_HEIGHT = 40;
        public const int VERTICAL_SPACE_HEIGHT = 5;

        public Menu(UiSystem uiSystem)
        {
            _uiSystem = uiSystem;
            _panel = new FixPanel(Anchor.Center, 0.25f, 0.5f);
            _panel.SetHeightBasedOnChildren = true;
            _panel.ChildPadding = (Padding) new Vector2(16);
            _background = Storage.Content.Load<Texture2D>("images/background");
        }
        
        public void Initialize()
        {
            _uiSystem.Add("background", new Image(Anchor.Center, Vector2.One, new TextureRegion(_background)));
            _uiSystem.Add("menu", _panel);
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
                    _panel.AddChild(new VerticalSpace(VERTICAL_SPACE_HEIGHT));
            
            _panel.AddChild(element);
        }
        
        
        public void Dispose()
        {
            _panel.RemoveChildren();
            _uiSystem.Remove("menu");
            _uiSystem.Remove("background");
        }
    }
}