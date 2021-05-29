using System.Collections.Generic;
using System.Dynamic;

namespace Askalhorn.Common.Inventory
{
    public interface IBag
    {
        IReadOnlyCollection<(IItem item, uint count)> Items { get; }

        float Weight { get; }
    }
}