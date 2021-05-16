using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public interface ICharacter
    {
        string Name { get; }
        
        IObservedParameter HP { get; }
        
        IObservedParameter MaxHP { get; }
        
        Texture2D Texture { get; }
        
        IPosition Position { get; }
    }
}