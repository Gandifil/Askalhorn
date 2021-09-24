using Askalhorn.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Characters.Builds
{
    internal class LootContainer: Container
    {
        public LootContainer(Character character)
            : base(character.Name, true, character.Bag)
        {
            Position = character.Position;
            Renderer = new TextureRenderer("grassland_tiles", new Point(0, 4));
        }

        public override bool IsStatic => false;
    }
}