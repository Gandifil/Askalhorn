using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    abstract internal class Ability: IAbility
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract TextureRegion2D Icon { get; }
        
        public abstract int CoolDown { get; }

        public int CoolDownTimer { get; private set; } = 0;
        public abstract int MagicCost { get; }

        void IAbility.Use(Character character, Character target)
        {
            CoolDownTimer = CoolDown;
            character.MP.Current.Value -= MagicCost;

            Use(character, target);
        }

        protected abstract void Use(Character character, Character target);
    }
}