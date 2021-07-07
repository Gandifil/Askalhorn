using System.Text;
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

        public bool IsReady => CoolDownTimer == 0;

        protected abstract void Use(Character character, Character target);

        public void Turn()
        {
            if (CoolDownTimer > 0)
                CoolDownTimer -= 1;
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Затраты магии: {MagicCost}");
            builder.AppendLine($"Откат: {CoolDown}");
            builder.AppendLine(Description);
            
            return builder.ToString();
        }
    }
}