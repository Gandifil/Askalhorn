using System;
using Askalhorn.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    
    public class DragAndDrop: Image
    {
        private const string ELEMENT_NAME = "DragAndDrop";
        
        public static event Action<DragAndDrop> OnDrop;

        public readonly IIcon Icon;
        
        public DragAndDrop(IIcon icon) : base(0, Vector2.Zero, icon.Renderer.Region.ToMlem(), true)
        {
            Icon = icon;
        }

        public void SuccesfullyDrop()
        {
            OnSuccesfullyDrop?.Invoke();
        }

        private void Drop()
        {
            OnDrop?.Invoke(this);
            Dispose();
        }
        
        public event Action OnSuccesfullyDrop;

        public void Show(UiSystem system)
        {
            system.Add(ELEMENT_NAME, this);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            
            var state = Mouse.GetState();
            PositionOffset = state.Position.ToVector2();
            if (state.LeftButton == ButtonState.Released)
                Drop();
        }
    }
}