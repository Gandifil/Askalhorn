using System.Collections.Generic;
using System.Dynamic;

namespace Askalhorn.Common.Inventory
{
    public interface IBag
    {
        IReadOnlyCollection<(IItem item, uint count)> Items { get; }

        void Put(IItem item, uint count = 1);
        IItem Pull(IItem item, uint count = 1);
        float Weight { get; }
    }
}