using System.Collections.Generic;

namespace Askalhorn.Plot
{
    public class StaticSpeech: ISpeech
    {
        public string Text { get; set; }      
        public IEnumerable<IAnswer> Answers { get; set; }   
    }
}