namespace Askalhorn.Plot
{
    public class Answer: IAnswer
    {
        public string Text { get; set; }
        
        public ISpeech Next { get; set; }
    }
}