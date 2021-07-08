using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics
{
    public interface IAbility
    {
        public enum TargetType
        {
            Self,
            Character
        }
        
        string Name { get; }

        string Description{ get; }
        
        TargetType Type { get; }

        TextureRegion2D Icon { get; }
        
        int CoolDown { get; }
        
        int Radius { get; }
        
        int CoolDownTimer { get; }
        
        int MagicCost { get; }
        
        bool IsReady { get; }
        
        uint MaxSkill { get; }
        
        uint Skill { get; }

        internal void Use(Character character, Character target);
    }
}