using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics
{
    public interface IAbility
    {
        public struct Modification
        {
            public string Description { get; set; }
            
            public TextureRegion2D Icon { get; set; }
        }
        
        public enum TargetType
        {
            Self,
            Character
        }
        
        string Name { get; }

        string Description{ get; }
        
        TargetType Type { get; }

        TextureRegion2D Icon { get; }
        
        SoundEffect CastSound { get; }
        
        int CoolDown { get; }
        
        int Radius { get; }
        
        int CoolDownTimer { get; }
        
        int MagicCost { get; }
        
        bool IsReady { get; }
        
        uint MaxSkill { get; }
        
        uint Skill { get; }
        
        List<Modification> Modifications { get;}
        
        int CurrentModification { get; set; }

        event Action OnChange;

        internal void Use(Character character, Character target);
    }
}