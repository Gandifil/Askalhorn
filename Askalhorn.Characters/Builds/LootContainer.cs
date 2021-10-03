using Askalhorn.Map;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Characters.Builds
{
    internal class LootContainer: Container
    {
        public LootContainer(Character character)
            : base(character.Name, true, character.Bag)
        {
            Position = (Position)Location.Current.Location.FindFreeSpaceForBuild(character.Position);
            Renderer = new TextureRenderer("builds/containers", new Point(0, 0));
        }

        public override bool IsStatic => false;
    }
}