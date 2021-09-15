using Askalhorn.Common;
using Askalhorn.Map.Impacts;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map.Builds
{
    class LocalTeleport: GameObject, IBuild
    {
        public IBuild.Types Type => IBuild.Types.Teleport;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings());

        public Point Target { get; set; }

        public IImpact Impact => new TeleportImpact(Target);
    }
}