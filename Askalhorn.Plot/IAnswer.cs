namespace Askalhorn.Plot
{
    public interface IAnswer
    {
        string Text { get; }

        ISpeech Next { get; }
    }
}