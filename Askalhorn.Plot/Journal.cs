using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Askalhorn.Plot
{
    public class Journal: IJournal
    {
        [JsonConstructor]
        public Journal(List<IQuest> quests)
        {
            Quests = quests;
        }

        public List<IQuest> Quests { get; }
        
        
        public IEnumerator<IQuest> GetEnumerator()
        {
            return Quests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Quests.GetEnumerator();
        }
    }
}