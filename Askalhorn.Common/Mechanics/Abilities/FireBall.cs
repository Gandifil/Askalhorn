using System.Collections.Generic;
using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Mechanics.Abilities
{
    internal class FireBall: Ability
    {
        public override string Name => "Огненная стрела";
        
        public override string Description => "Обычный огненный шар, который обычно летит прямо в ебало.";
        public override TextureRegion2D Icon => Storage.Load("effects", 0, 1);
        public override IAbility.TargetType Type => IAbility.TargetType.Character;
        public override int Radius => 10;
        public override int CoolDown { get; } = 0;
        public override int MagicCost => CurrentModification == 3 ? 50 : 100;
        public override uint MaxSkill => 10;
        public override SoundEffect CastSound => Storage.LoadSound("steam");

        public override List<IAbility.Modification> Modifications { get; } =
            new()
            {
                new IAbility.Modification()
                {
                    Description = "Истинное разрушение\nСтрела наносит значительно больше урона.",
                    Icon = Storage.Load("effects", 2, 1),
                },
                new IAbility.Modification()
                {
                    Description = "Поджог\nСтрела дополнительно поджигает противника, нанося 10 единиц урона в течении 2 секунд",
                    Icon = Storage.Load("effects", 3, 1),
                },
                new IAbility.Modification()
                {
                    Description = "Родство с огнем\nПридавать огню форму стрелы становится значительно легче, благодаря чему затраты магии снижаются в 2 раза.",
                    Icon = Storage.Load("effects", 4, 1),
                },
            };
        
        protected override void Use(Character character, Character target)
        {
            new DamageImpact(10).On(target);
        }
    }
}