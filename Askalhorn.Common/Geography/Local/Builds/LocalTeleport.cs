using System;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Geography.Local.Builds
{
    class LocalTeleport: HasPosition, IBuild
    {
        IPosition IBuild.Position => Position;

        IRenderer IBuild.Renderer => Renderer;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings());

        public Point Target { get; set; }

        public Action Action => () =>
        {
            Log.Information("Used local teleport with shift {Target}", Target);

            var teleport = new MovementToMove(Target);
            Common.World.Instance.playerController.AddMove(teleport);
        };
    }
}