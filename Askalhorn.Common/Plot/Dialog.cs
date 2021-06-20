namespace Askalhorn.Common.Plot
{
    public class Dialog
    {
        public class Answer
        {
            public string Line { get; set; }
            public uint Target { get; set; }
        }
        
        public class Speech
        {
            public string[] Lines { get; set; }
            public Answer[] Answers { get; set; }
        }

        public Speech[] Speeches { get; set; }
    }
}