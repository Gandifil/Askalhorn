using System;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        //public Panel Panel => _panel;

        public const int ELEMENT_HEIGHT = 40;
        public const int VERTICAL_SPACE_HEIGHT = 5;

        public Menu(UiSystem uiSystem)
        {
            _uiSystem = uiSystem;
            _panel = new Panel(Anchor.Center, new Vector2(0.25f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
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

        private bool isEmptyPanel = true;

        public void Add(Element element)
        {
            if (isEmptyPanel)
                isEmptyPanel = false;
            else
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