using System.Linq;
using Askalhorn.Common.Mechanics;
using Serilog;

namespace Askalhorn.Common.Control.Moves
{
    public class UseAbilityMove: IMove
    {
        public readonly IAbility Ability;

        public ICharacter Target { get; set; } 

        public UseAbilityMove(IAbility ability)
        {
            this.Ability = ability;
        }
        
        public bool IsValid(ICharacter character)
        {
            return character.Abilities.Contains(Ability)
                && character.MP.Current.Value >= Ability.MagicCost
                && Ability.IsReady;
        }

        void IMove.Make(Character character)
        {
            var itemName = Ability.Name;
            Log.Information("{Name} использовал {itemName}", character.Name, itemName);
            Ability.CastSound.Play();
            
            Ability.Use(character, (Character) Target);
        }
    }
}