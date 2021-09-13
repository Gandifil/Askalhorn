using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Builds
{
    class LocalTeleport: HasPosition, IBuild
    {
        public IBuild.Types Type => IBuild.Types.Teleport;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings());

        public Point Target { get; set; }

        public IImpact Impact => new TeleportImpact(Target);
    }
}