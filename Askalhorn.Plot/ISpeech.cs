using System;
using System.Collections.Generic;
using System.Text;

namespace Askalhorn.Plot
{
    public interface ISpeech
    {
        string Text { get; }

        IEnumerable<IAnswer> Answers { get; }
    }
}
