using System.Linq;
using Askalhorn.Common;
using Askalhorn.Dialogs;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Content;
using MonoGame.Extended.Input.InputListeners;
using Serilog;

namespace Askalhorn.UI.Dialogs
{
    public class DialogViewer: FixPanel
    {
        private readonly DialogEnumerator _enumerator;
        
        private readonly Image _image;

        private readonly MenuIB _answers;

        private readonly MLEM.Ui.Elements.Paragraph _text;
        
        public DialogViewer(DialogEnumerator dialog, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            _enumerator = dialog;

            _text = new CustomText(Anchor.TopLeft, .5f, "");
            AddChild(_text);

            _image = new Image(Anchor.CenterRight, new Vector2(200, 200), _enumerator.Renderer.Region.ToMlem(), true);
            AddChild(_image);

            _answers = new MenuIB(Anchor.BottomLeft, .5f, .5f);
            AddChild(_answers);

            UpdateState();
            _enumerator.OnChanded += UpdateState;
            _enumerator.OnEnd += this.Close;
            Log.Debug("DialogViewer init");
            
            InputListeners.Input.KeyboardListener.Push(new NumericKeyboardListener());
            InputListeners.Input.MouseListener.Push(new MouseListener());
            InputListeners.Keyboard.NumericKeyReleased += OnNumericKeyReleased;
        }

        public override void Dispose()
        {
            InputListeners.Keyboard.NumericKeyReleased -= OnNumericKeyReleased;
            InputListeners.Input.KeyboardListener.Pop();
            InputListeners.Input.MouseListener.Pop();
            
            _enumerator.OnChanded -= UpdateState;
            _enumerator.OnEnd -= this.Close;
            
            Log.Debug("DialogViewer disposed");
            
            base.Dispose();
        }

        private void OnNumericKeyReleased(object? sender, int number)
        {
            number--;
            if (number < _enumerator.Answers.Count())
                _enumerator.Choose(_enumerator.Answers.ElementAt(number));
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            
            _enumerator.Skip(time.ElapsedGameTime.Milliseconds);
        }

        private void UpdateState()
        {
            _text.Text = _enumerator.CurrentParagraph.Text;
            _image.Texture = _enumerator.Renderer.Region.ToMlem();
            
            _answers.Clear();
            foreach (var answer in _enumerator.Answers)
                _answers.AddButton(answer.Line, () => { _enumerator.Choose(answer);});
        }
    }
}