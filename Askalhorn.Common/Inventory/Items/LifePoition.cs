﻿using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    class LifePoition: IItem
    {
        public readonly int Value;

        public LifePoition(int value)
        {
            Value = value;
        }
        public string Name => $"Лечебное зелье +{Value}";
        public string Description => $"Восстанавливает {Value} HP";
        public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            0, 0, 32, 32);

        public float Weight => 0.5f;
        public Size Size { get; } = new Size(1, 1);

        IImpact IItem.Impact => new HealImpact(Value);
    }
}