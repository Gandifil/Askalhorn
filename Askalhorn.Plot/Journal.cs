using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Text;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Plot
{
    public class Journal: IJournal
    {
        private readonly List<IQuest> _quests = new List<IQuest>();
            
        [JsonConstructor]
        public Journal(List<IQuest> quests)
        {
            _quests = quests;
        }

        public void Add(IQuest quest)
        {
            if (_quests.Exists(x => x.Equals(quest)))
                throw new ArgumentException($"Character yet has quest {quest.Name}");
            
            _quests.Add(quest);
        }
        
        public IEnumerator<IQuest> GetEnumerator()
        {
            return _quests.GetEnumerator();
        }

        IQuest IJournal.Find(string name)
        {
            return Find(name);
        }

        public Quest Find(string name)
        {
            return _quests.Select(x => x as Quest).First(x => x.ContentName == name);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _quests.GetEnumerator();
        }
    }
}