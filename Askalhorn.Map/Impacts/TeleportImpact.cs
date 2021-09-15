using System;
using Askalhorn.Common;
using Askalhorn.Map.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Map.Impacts
{
    public class TeleportImpact: IImpact
    {
        public string Description { get; } = null;
        public TextureRegion2D TextureRegion { get; } = null;
        public readonly Point Aim;

        [JsonConstructor]
        public TeleportImpact(Point aim)
        {
            Aim = aim;
        }
        
        public void On(object target)
        {
            var gameObject = target as GameObject;
            if (gameObject is null)
                throw new ArgumentNullException(nameof(target));

            gameObject.Position = new Position(Aim);
        }
    }
}