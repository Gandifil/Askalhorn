using System.Linq;

namespace Askalhorn.Common.Plot
{
    public class DialogInterpereter
    {
        private readonly Dialog dialog;
        private Dialog.Speech current;

        public DialogInterpereter(Dialog dialog)
        {
            this.dialog = dialog;
            Answer(0);
        }
        
        public string[] SpeechLines => current.Lines;
        
        public string[] Answers => current.Answers.Select(x => x.Line).ToArray();

        public void Answer(uint index)
        {
            current = dialog.Speeches[index];
        }
    }
}