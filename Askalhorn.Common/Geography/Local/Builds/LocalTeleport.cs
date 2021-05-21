using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local.Builds
{
    class LocalTeleport: IBuild
    {
        IPosition IBuild.Position => Position;

        public Position Position { get; set; } = new Position(10, 10);


        IRenderer IBuild.Renderer => Renderer;

        public IRenderer Renderer { get; set; } = new ParticleRenderer();
    }
}