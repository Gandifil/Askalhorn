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

        public IRenderer Renderer { get; set; } = new ParticleRenderer();

        public Point Shift { get; set; }

        public Action Action => () =>
        {
            Log.Information("Used local teleport with shift {Shift}", Shift);

            var teleport = new MovementMove(Shift);
            Common.World.Instance.playerController.AddMove(teleport);
            Common.World.Instance.Turn();
        };
    }
}