using System.Collections.Generic;

namespace Askalhorn.Common.Plot
{
    public interface ISpeech
    {
        string Text { get; }

        IEnumerable<IAnswer> Answers { get; }
    }
}
