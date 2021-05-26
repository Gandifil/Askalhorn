using System.Collections.Generic;
using System.Collections.ObjectModel;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public interface ICharacter
    {
        string Name { get; }
        
        ILimitedValue<IObservedParameter<int>> HP { get; }
        
        IObservedParameter<uint> Level { get; }
        
        IObservedParameter<int> Strength { get; }

        Texture2D Texture { get; }
        
        IPosition Position { get; }
        
        IEnumerable<IPosition> CanMoveTo { get; }
        
        IEnumerable<IAbility> Abilities { get; }
    }
}