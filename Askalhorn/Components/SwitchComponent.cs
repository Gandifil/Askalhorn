using System;
using System.Linq;
using AmbrosiaGame.Screens;
using Microsoft.Xna.Framework;

namespace Askalhorn.Components
{
    public class SwitchComponent: IGameComponent, IDisposable
    {
        private IGameComponent component;
        
        private readonly GameProcessScreen screen;

        public SwitchComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
        }

        public void SwitchTo<T>() where T:IGameComponent
        { 
            if (component is null || component is not T)
            {
                Dispose();
                component = (T)Activator.CreateInstance(typeof(T), screen);
                component.Initialize();
            }
            else
                Dispose();
        }

        public void SwitchTo<T>(T newComponent) where T : IGameComponent
        {
            if (component is null || component is not T)
            {
                Dispose();
                component = newComponent;
                component.Initialize();
            }
            else
                Dispose();
        }

        public void Dispose()
        {
            (component as IDisposable)?.Dispose();
            component = null;
        }
    }
}