using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Render;

namespace Askalhorn.Dialogs
{
    public sealed class DialogEnumerator
    {
        private readonly Dialog _dialog;
        private int _millisecondsCounter = 0;

        public DialogEnumerator(Dialog dialog)
        {
            _dialog = dialog;
            CurrentSpeech = GetStartSpeech();
        }

        private Speech GetStartSpeech()
        {
            return _dialog.Speeches.Where(x => x.IsStart).First();
        }

        public void Choose(Answer answer)
        {
            if (0 <= answer.Target && answer.Target < _dialog.Speeches.Length)
                CurrentSpeech = _dialog.Speeches[answer.Target];
            else
                OnEnd?.Invoke();
        }

        public void Skip(int milliseconds)
        {
            _millisecondsCounter += milliseconds;

            if (CurrentParagraph.ShowMilliseconds < _millisecondsCounter)
            {
                _millisecondsCounter = 0;
                if (CurrentParagraphIsLast)
                {
                    if (!Answers.Any())
                        OnEnd?.Invoke();
                }
                else
                {
                    _paragraphIndex++;
                    OnChanded?.Invoke();
                }
            }
        }

        public event Action OnChanded;

        public event Action OnEnd;

        private Speech _currentSpeech;

        public Speech CurrentSpeech
        {
            get => _currentSpeech;
            private set
            {
                _currentSpeech = value;
                _millisecondsCounter = 0;
                _paragraphIndex = 0;
                OnChanded?.Invoke();
            }
        }

        public IList<Answer> Answers => CurrentParagraphIsLast ? CurrentSpeech.Answers : new List<Answer>();

        private uint _paragraphIndex = 0;
        public Paragraph CurrentParagraph => CurrentSpeech.Paragraphs[_paragraphIndex];

        public bool CurrentParagraphIsLast => _paragraphIndex + 1 == CurrentSpeech.Paragraphs.Length;

        public TextureRenderer Renderer => CurrentParagraph.Renderer ?? CurrentSpeech.Renderer ?? _dialog.Renderer;
    }
}