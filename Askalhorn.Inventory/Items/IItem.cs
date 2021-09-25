using System;
using Askalhorn.Common;
using Askalhorn.Render;

namespace Askalhorn.Inventory.Items
{
    
    public interface IItem: IIcon, IEquatable<IItem>
    {
        string Name { get; }
        
        ItemPurpose Type { get; }
        
        ItemRarity ItemRarity { get; }
        
        float Weight { get; }
        
        IImpact Impact { get; }
    }
}