using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Askalhorn.Common.Mechanics.Effects
{
    internal class Pool: List<Effect>
    {
        //private readonly List<Effect> effects = new List<Effect>();
        private readonly Character character;

        public Pool(Character character) 
        {
            this.character = character;
            
        }
        
        public void Add(Effect effect)
        {
            effect.Subscribe(character);
            base.Add(effect);
        }

        public void Turn()
        {
            foreach (var effect in this)
                effect.Turn(character);
            
            foreach (var effect in this.Where(x => !x.IsAlive))
                effect.Unsubscribe(character);

            RemoveAll(x => !x.IsAlive);
        }
    }
}