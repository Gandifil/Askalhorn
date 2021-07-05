using System;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Components
{
    public interface IActionBlock
    {
        public TextureRegion2D Region { get; }
        
        Action Action { get; }

        Keys Key { get; }
    }
}