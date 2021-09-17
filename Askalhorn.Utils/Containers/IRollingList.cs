using System;
using System.Collections.Generic;

namespace Askalhorn.Utils.Containers
{
    public interface IRollingList: IReadOnlyList<string>
    {
        void Write(string line);

        event Action<string> OnLineWrote;

        event Action OnLineRemoved;
    }
}