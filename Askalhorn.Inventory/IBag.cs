﻿using System;
using System.Collections.Generic;

namespace Askalhorn.Inventory
{
    public interface IBag
    {
        IReadOnlyCollection<Pack> Items { get; }

        void Put(IItem item, uint count = 1);
        IItem Pull(IItem item, uint count = 1);
        float Weight { get; }
        public event Action OnChanged;
    }
}