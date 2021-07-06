using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class Strike: IAbility
    {
        public string Name => "Удар";
        
        public string Description => "Удар";
        public Texture2D Icon => Storage.Content.Load<Texture2D>("images/fireball");
        void IAbility.Use(Character character, Character target)
        {
            new DamageImpact(10).On(target);
        }
    }
}