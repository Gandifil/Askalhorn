using System.Linq;
using Serilog;

namespace Askalhorn.Characters.Control.Moves
{
    public class UseAbilityMove: IMove
    {
        public IAbility Ability { get; set; } 

        public ICharacter Target { get; set; }

        public bool IsReady => Ability.CoolDownTimer == 0;

        public bool IsEnoughMagic(ICharacter character)
        {
            return character.MP.Current.Value >= Ability.MagicCost;
        }
        
        public bool IsValid(ICharacter character)
        {
            return character.Abilities.Contains(Ability)
                && IsEnoughMagic(character)
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