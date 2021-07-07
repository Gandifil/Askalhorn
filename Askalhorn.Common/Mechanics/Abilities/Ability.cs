using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    abstract internal class Ability: IAbility
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public TextureRegion2D Icon { get; protected set; }
        
        public int CoolDown { get; protected set; }

        public int CoolDownTimer { get; private set; } = 0;
        
        void IAbility.Use(Character character, Character target)
        {
            CoolDownTimer = CoolDown;
        }

        protected abstract void Use(Character character, Character target);
    }
}