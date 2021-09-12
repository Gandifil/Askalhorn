using System;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Geography.Local.Builds
{
    class LocalTeleport: HasPosition, IBuild
    {
        IPosition IBuild.Position => Position;

        public IBuild.Types Type => IBuild.Types.Teleport;

        IRenderer IBuild.Renderer => Renderer;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings());

        public Point Target { get; set; }

        public IImpact Impact => new TeleportImpact(Target);
    }
}