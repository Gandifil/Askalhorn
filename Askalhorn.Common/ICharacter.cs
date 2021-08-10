using System.Collections.Generic;
using System.Collections.ObjectModel;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Utils;
using Askalhorn.Common.Plot;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public interface ICharacter
    {
        string Name { get; }
        
        ILimitedValue<IObservedParameter<int>> HP { get; }
        
        ILimitedValue<IObservedParameter<int>> MP { get; }
        
        ILinearParameter<int> Level { get; }
        
        IAttributes<PrimaryTypes> Primary { get; }
        
        IReadOnlyDictionary<PrimaryTypes, int> PrimaryBase { get; }
        
        IAttributes<SecondaryTypes> Secondary { get; }
        
        IReadOnlyDictionary<SecondaryTypes, int> SecondaryBase { get; }
        
        IAttributes<DamageTypes> Protection { get; }
        
        IReadOnlyDictionary<DamageTypes, int> ProtectionBase { get; }

        IRenderer Renderer { get; }
        
        IPosition Position { get; }
        
        IEnumerable<MovementMove> AvailableMovements { get; }
        
        IEnumerable<IAbility> Abilities { get; }
        
        IReadOnlyCollection<IEffect> Effects { get; }
        
        Dialog Dialog { get; }

        IBag Bag { get; }

        Costume Costume { get; }
    }
}