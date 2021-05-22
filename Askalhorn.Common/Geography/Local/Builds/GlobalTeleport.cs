using System;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Geography.Local.Builds
{
    class GlobalTeleport: HasPosition, IBuild
    {
        IPosition IBuild.Position => Position;

        IRenderer IBuild.Renderer => Renderer;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings()
        {
            StartColor = Color.BlueViolet,
            EndColor = Color.Red,
            Capacity = 600,
            Radius = 40,
        });

        public Point Shift { get; set; }

        public Action Action => () =>
        {
            Log.Information("Used global teleport with shift {Shift}", Shift);

            var teleport = new MovementToMove(Shift);
            Common.World.Instance.playerController.AddMove(teleport);
            Common.World.Instance.Turn();
        };
    }
}