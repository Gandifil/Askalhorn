using System;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Render;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory
{
    
    public interface IItem: IIcon, IEquatable<IItem>
    {
        public enum PurposeType
        {
            Head,
            Cloak,
            Body,
            Hands,
            Boots,
            Amulet,
            Bracelet,
            Ring,
            Shield,
            Weapon,
            Poition,
            Resource,
        }

        public enum RarityLevel
        {
            Casual,
            Rare,
            Unique,
        }
        
        string Name { get; }
        
        PurposeType Type { get; }
        
        RarityLevel Rarity { get; }
        
        //string Description { get; }
        
        //TextureRegion2D Texture { get; }
        
        float Weight { get; }
        
       // Size Size { get; }
        
        internal IImpact Impact { get; }
    }
}