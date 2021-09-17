using Askalhorn.Common;
using Askalhorn.Map.Actions;
using Askalhorn.Map.Impacts;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map.Builds
{
    class LocalTeleport: GameObject, IBuild, IActionable
    {
        public IBuild.Types Type => IBuild.Types.Teleport;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings());

        public Point Target { get; set; }

        public IAction Action => new TransferAction(new TeleportImpact(Target));
    }
}