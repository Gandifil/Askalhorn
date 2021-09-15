using System;

namespace Askalhorn.Inventory
{
    public interface ILootChooser
    {
        void Fill(Random random, IBag bag);
    }
}