using System;
using Askalhorn.Characters.Abilities;
using Askalhorn.Common;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Characters.Impacts
{
    public class AddAbilityImpact: IImpact
    {
        public string Description { get; }
        
        public TextureRegion2D TextureRegion { get; }

        public string Name { get; }

        [JsonConstructor]
        [CommandConstructor]
        public AddAbilityImpact(string name)
        {
            Name = name;
        }
        
        public void On(object target)
        {
            var character = target as Character;
            if (character is null)
                throw new ArgumentNullException(nameof(target));

            var type = Type.GetType($"Askalhorn.Characters.Abilities.{Name}, Askalhorn.Characters");
            var ability = Activator.CreateInstance(type) as Ability;
            character.Abilities.Add(ability);
            
            Log.Information(new TextPointer("ability", "AddAbility").ToString(), ability.Name);
        }
    }
}