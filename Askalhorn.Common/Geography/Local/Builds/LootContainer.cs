﻿using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local.Builds
{
    internal class LootContainer: Container
    {
        public LootContainer(Character character)
            : base(character.Name, true, character.Bag)
        {
            Position = character.Position;
            Renderer = new TextureRenderer("images/grassland_tiles", 0, 256, 64, 64);
        }
    }
}