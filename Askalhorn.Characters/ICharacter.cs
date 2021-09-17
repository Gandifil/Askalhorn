using System.Collections.Generic;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Dialogs;
using Askalhorn.Inventory;
using Askalhorn.Map;
using Askalhorn.Map.Actions;
using Askalhorn.Utils;

namespace Askalhorn.Characters
{
    public interface ICharacter: IGameObject, IActionable
    {
        string Name { get; }
        
        IFraction Fraction { get; }
        
        ILimitedValue<IObservedParameter<int>> HP { get; }
        
        ILimitedValue<IObservedParameter<int>> MP { get; }
        
        ILinearParameter<int> Level { get; }
        
        IAttributes<PrimaryTypes> Primary { get; }
        
        IReadOnlyDictionary<PrimaryTypes, int> PrimaryBase { get; }
        
        IAttributes<SecondaryTypes> Secondary { get; }
        
        IReadOnlyDictionary<SecondaryTypes, int> SecondaryBase { get; }
        
        IAttributes<DamageTypes> Protection { get; }
        
        IReadOnlyDictionary<DamageTypes, int> ProtectionBase { get; }
        
        IEnumerable<MovementMove> AvailableMovements { get; }
        
        IEnumerable<IAbility> Abilities { get; }
        
        IReadOnlyCollection<IEffect> Effects { get; }
        
        Dialog Dialog { get; }

        IBag Bag { get; }

        Costume Costume { get; }
    }
}