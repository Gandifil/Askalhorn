using System.Collections.Generic;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Characters.Effects;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Dialogs;
using Askalhorn.Inventory;
using Askalhorn.Map;
using Askalhorn.Map.Actions;
using Askalhorn.Utils;

namespace Askalhorn.Characters
{
    public interface ICharacter: IGameObject, IActionable, IHasBag
    {
        string Name { get; }
        
        IFraction Fraction { get; }
        
        ILimitedValue<IObservedParameter<int>> HP { get; }
        
        ILimitedValue<IObservedParameter<int>> MP { get; }
        
        ILinearParameter<int> Level { get; }
        
        IAttributes<PrimaryType> Primary { get; }
        
        IReadOnlyDictionary<PrimaryType, int> PrimaryBase { get; }
        
        IAttributes<SecondaryType> Secondary { get; }
        
        IReadOnlyDictionary<SecondaryType, int> SecondaryBase { get; }
        
        IAttributes<DamageType> Protection { get; }
        
        IReadOnlyDictionary<DamageType, int> ProtectionBase { get; }
        
        IEnumerable<MovementMove> AvailableMovements { get; }
        
        IReadOnlyCollection<IAbility> Abilities { get; }
        
        IEffectPool EffectPool { get; }
        
        Dialog Dialog { get; }

        Costume Costume { get; }
    }
}