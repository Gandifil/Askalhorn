using Askalhorn.Common.Interpetators;

namespace Askalhorn.Dialogs
{
    public class Answer
    {
        public string Line { get; set; }
        
        public int Target { get; set; }

        public BoolExpression Requirement { get; set; }
    }
}