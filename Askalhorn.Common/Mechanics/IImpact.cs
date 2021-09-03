using System.Security.Cryptography;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics
{
    public interface IImpact
    {
        string Description { get; }
        
        TextureRegion2D  TextureRegion { get; }
        
        void On(Character character);
    }
}