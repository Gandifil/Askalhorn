using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics
{
    public interface IAbility
    {
        string Name { get; }

        string Description{ get; }

        TextureRegion2D Icon { get; }
        
        int CoolDown { get; }
        
        int CoolDownTimer { get; }
        
        int MagicCost { get; }

        internal void Use(Character character, Character target);
    }
}