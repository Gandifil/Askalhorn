using Askalhorn.Common.Control.Moves;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    public class TeleportImpact: IImpact
    {
        public string Description { get; } = null;
        public TextureRegion2D TextureRegion { get; } = null;
        public readonly Point Target;

        [JsonConstructor]
        public TeleportImpact(Point target)
        {
            Target = target;
        }
        
        public void On(Character character)
        {
            Log.Information("Used local teleport with shift {Target}", Target);

            var teleport = new MovementToMove(Target);
            World.Instance.playerController.AddMove(teleport);
        }
    }
}