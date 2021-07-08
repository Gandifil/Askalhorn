using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class Strike: Ability
    {
        public override string Name => "Удар";
        
        public override string Description => "Удар";
        public override TextureRegion2D Icon => new TextureRegion2D(Storage.Content.Load<Texture2D>("images/fireball"));
        public override IAbility.TargetType Type => IAbility.TargetType.Character;
        public override int Radius => 1;
        public override int CoolDown { get; } = 0;
        public override int MagicCost => 100;
        public override uint MaxSkill => 10;

        protected override void Use(Character character, Character target)
        {
            new DamageImpact(10).On(target);
        }
    }
}