using System;
using System.Collections.Generic;

namespace Askalhorn.Utils.Containers
{
    public class RAMRollingList: List<string>, IRollingList
    {
        public int MaxCount { get; }

        public RAMRollingList(int maxCount)
        {
            MaxCount = maxCount;
        }
        
        public void Write(string line)
        {
            Add(line);
            if (Count > MaxCount)
                RemoveFirst();
                
            OnLineWrote?.Invoke(line);
        }

        private void RemoveFirst()
        {
            RemoveAt(0);
            OnLineRemoved?.Invoke();
        }

        public event Action<string> OnLineWrote;
        public event Action OnLineRemoved;
    }
}