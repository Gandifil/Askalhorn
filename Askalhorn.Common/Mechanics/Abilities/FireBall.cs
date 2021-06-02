using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class FireBall: IAbility
    {
        public string Name => "Огненный шар";
        
        public string Description => "Обычный огненный шар, который обычно летит прямо в ебало.";
        public Texture2D Icon => Storage.Content.Load<Texture2D>("images/fireball");
        void IAbility.Use(Character character, Character target)
        {
            new DamageImpact(10).On(target);
        }
    }
}