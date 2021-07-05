using System.Collections.Generic;
using System.Collections.ObjectModel;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Utils;
using Askalhorn.Common.Plot;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public interface ICharacter
    {
        string Name { get; }
        
        ILimitedValue<IObservedParameter<int>> HP { get; }
        
        ILinearParameter<int> Level { get; }
        
        IAttributes<PrimaryTypes> Primary { get; }
        
        IReadOnlyDictionary<PrimaryTypes, int> PrimaryBase { get; }

        Texture2D Texture { get; }
        
        IPosition Position { get; }
        
        IEnumerable<MovementMove> AvailableMovements { get; }
        
        IEnumerable<IAbility> Abilities { get; }
        
        IReadOnlyCollection<IEffect> Effects { get; }
        
        Dialog Dialog { get; }

        IBag Bag { get; }
    }
}