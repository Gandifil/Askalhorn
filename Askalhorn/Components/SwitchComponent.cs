using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Components
{
    public class SwitchComponent: IGameComponent, IDisposable
    {
        private IGameComponent component;
        
        public void Initialize()
        {
        }

        public void SwitchTo<T>() where T:IGameComponent
        {
            Dispose();
            
            if (component is not T)
            {
                    
                
            }
        }

        public void Dispose()
        {
            (component as IDisposable)?.Dispose();
        }
    }
}