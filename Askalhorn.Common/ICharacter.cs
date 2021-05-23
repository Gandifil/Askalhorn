using System.Collections.Generic;
using System.Collections.ObjectModel;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public interface ICharacter
    {
        string Name { get; }
        
        IObservedParameter<uint> HP { get; }
        
        IObservedParameter<uint> MaxHP { get; }
        
        Texture2D Texture { get; }
        
        IPosition Position { get; }
        
        IEnumerable<IPosition> CanMoveTo { get; }
        
        IEnumerable<IAbility> Abilities { get; }
    }
}