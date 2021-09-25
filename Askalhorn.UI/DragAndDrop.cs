using System;
using Askalhorn.Render;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI
{
    
    public class DragAndDrop: Image
    {
        public static event Action<DragAndDrop> OnDrop;

        public readonly IIcon Icon;
        
        public DragAndDrop(IIcon icon) : base(0, Vector2.Zero, icon.Renderer.Region.ToMlem(), true)
        {
            Icon = icon;
            InputListeners.Mouse.MouseDragEnd += OnMouseDragEnd;
            InputListeners.Input.MouseListener.ListenerRemoved += OnListenerRemoved;
        }

        private void OnListenerRemoved(MouseListener obj)
        {
            this.Close();
        }

        public override void Dispose()
        {
            InputListeners.Mouse.MouseDragEnd -= OnMouseDragEnd;
            InputListeners.Input.MouseListener.ListenerRemoved -= OnListenerRemoved;
            
            base.Dispose();
        }

        private void OnMouseDragEnd(object? sender, MouseEventArgs e)
        {
            OnDrop?.Invoke(this);
            this.Close();
        }

        public void SuccesfullyDrop()
        {
            OnSuccesfullyDrop?.Invoke();
        }
        
        public event Action OnSuccesfullyDrop;
        public override void Update(GameTime time)
        {
            base.Update(time);
            
            var state = Mouse.GetState();
            PositionOffset = state.Position.ToVector2();
        }

        private const string ELEMENT_NAME = "DragAndDrop";
        
        public void Show(UiSystem system)
        {
            system.Add(ELEMENT_NAME, this);
        }
    }
}