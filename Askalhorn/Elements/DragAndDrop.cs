using System;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Extended.Extensions;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    
    public class DragAndDrop: Image
    {
        private const string ELEMENT_NAME = "DragAndDrop";
        
        public static event Action<DragAndDrop> OnDrop;

        public readonly IIcon Icon;
        
        public DragAndDrop(IIcon icon) : base(0, Vector2.Zero, icon.Texture.ToMlem(), true)
        {
            Icon = icon;
            OnUpdated += Update;
        }

        public void SuccesfullyDrop()
        {
            OnSuccesfullyDrop?.Invoke();
        }
        
        public event Action OnSuccesfullyDrop;

        public void Show()
        {
            AskalhornGame.Instance.UiSystem.Add(ELEMENT_NAME, this);
        }

        private static void Update(Element element, GameTime time)
        {
            var state = Mouse.GetState();
            element.PositionOffset = state.Position.ToVector2();
            if (state.LeftButton == ButtonState.Released)
            {
                OnDrop?.Invoke(element as DragAndDrop);
                AskalhornGame.Instance.UiSystem.Remove(ELEMENT_NAME);
            }
        }
    }
}