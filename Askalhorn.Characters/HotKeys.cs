using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Newtonsoft.Json;

namespace Askalhorn.Characters
{
    public class HotKeys
    {
        private readonly int?[] _bindings;

        public IReadOnlyList<int?> Bindings => _bindings;
        
        public HotKeys()
        {
            _bindings = new int?[10];
        }
        
        [JsonConstructor]
        public HotKeys(int?[] bindings)
        {
            _bindings = bindings;
        }

        public IAbility[] GetAbilities(ICharacter character)
        {
            var list = character.Abilities.ToList();
            var results = new IAbility[10];

            for (int i = 0; i < results.Length; i++)
                results[i] = Bindings[i].HasValue ? list[Bindings[i].Value] : null;

            return results;
        }

        public void Set(ICharacter character, IAbility ability, int index)
        {
            _bindings[index] = character.Abilities.ToList().IndexOf(ability);
            BindingsChanged?.Invoke();
        }

        public void Clear(int index)
        {
            _bindings[index] = null;
            BindingsChanged?.Invoke();
        }

        public event Action BindingsChanged;
    }
}