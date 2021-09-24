using System.Collections.Generic;

namespace Askalhorn.Plot
{
    public interface IJournal: IEnumerable<IQuest>
    {
        IQuest Find(string name);
    }
}