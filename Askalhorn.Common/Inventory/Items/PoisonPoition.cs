﻿using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    internal class PoisonPoition: IItem
    {
        public readonly int Value;

        public PoisonPoition(int value)
        {
            Value = value;
        }
        public string Name => $"Ядовитое зелье -{Value}";
        public string Description => $"Забирает {Value} HP";
        public TextureRegion2D Texture { get; } = new TextureRegion2D(Storage.Content.Load<Texture2D>("images/items"), 
            2*32, 0, 32, 32);

        public float Weight => 0.5f;
        public Size Size { get; } = new Size(1, 1);

        IImpact IItem.Impact => new DamageImpact(Value);
    }
}