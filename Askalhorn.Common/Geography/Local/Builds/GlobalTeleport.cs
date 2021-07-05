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

        public IBuild.Types Type => IBuild.Types.Teleport;

        IRenderer IBuild.Renderer => Renderer;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings()
        {
            StartColor = Color.BlueViolet,
            EndColor = Color.Red,
            Capacity = 600,
            Radius = 40,
        });

        public LocationInfo Location { get; set; }

        public uint Place { get; set; }

        public Action Action => () =>
        {
            //Log.Information("Used global teleport with shift {Shift}", Shift);

            Common.World.Instance.SetLocation(Location, Place);
        };
    }
}