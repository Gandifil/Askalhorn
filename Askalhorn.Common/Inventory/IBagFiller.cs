using System;

namespace Askalhorn.Common.Inventory
{
    public interface IBagFiller
    {
        void Fill(Random random, IBag bag);
    }
}