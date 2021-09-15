using System.Collections.Generic;
using Askalhorn.Text;

namespace Askalhorn.Plot
{
    public class QuestFile
    {
        public TextPointer Name { get; set; }
        
        public TextPointer Description { get; set; }
        
        public List<QuestStep> Steps { get; set; }
    }
}