using System.Collections.Generic;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Abilities
{
    public class Strike: Ability
    {
        public override string Name => "Удар";
        
        public override string Description => "Удар";
        public override TextureRegion2D Icon => Storage.Load("effects", 1, 1);
        public override IAbility.TargetType Type => IAbility.TargetType.Character;
        public override int Radius => 1;
        public override int CoolDown => 0;
        public override int MagicCost => 100;
        public override uint MaxSkill => 10;
        public override SoundEffect CastSound => Storage.LoadSound("forcepush");

        public override List<IAbility.Modification> Modifications { get; } =
            new List<IAbility.Modification>();

        public override void Use(Character character, Character target)
        {
            new DamageImpact(10).On(target);
        }
    }
}