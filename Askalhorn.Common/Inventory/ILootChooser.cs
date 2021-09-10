using System;

namespace Askalhorn.Common.Inventory
{
    public interface ILootChooser
    {
        void Fill(Random random, IBag bag);
    }
}