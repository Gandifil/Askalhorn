using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class FireBall: IAbility
    {
        public string Name => "Огненный шар";
        
        public string Description => "Обычный огненный шар, который обычно летит прямо в ебало.";
        public TextureRegion2D Icon => new TextureRegion2D(Storage.Content.Load<Texture2D>("images/fireball"));
        public int CoolDown { get; } = 0;
        public int CoolDownTimer { get; } = 0;
        public int MagicCost => 100;

        void IAbility.Use(Character character, Character target)
        {
            new DamageImpact(10).On(target);
        }
    }
}