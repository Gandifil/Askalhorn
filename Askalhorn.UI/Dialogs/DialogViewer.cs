using Askalhorn.Common;
using Askalhorn.Dialogs;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Content;
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

            _text = new MLEM.Ui.Elements.Paragraph(Anchor.TopLeft, Storage.Content.GetGraphicsDevice().Viewport.Width / 2, "", true);
            AddChild(_text);

            _image = new Image(Anchor.CenterRight, new Vector2(200, 200), _enumerator.Renderer.Region.ToMlem(), true);
            AddChild(_image);

            _answers = new MenuIB(Anchor.BottomLeft, .5f, .5f);
            AddChild(_answers);

            UpdateState();
            _enumerator.OnChanded += UpdateState;
            _enumerator.OnEnd += Close;
            Log.Debug("DialogViewer init");
        }

        private void Close()
        {
            if (Root.Element == this)
                System.Remove(Root.Name);
        }

        public override void Dispose()
        {
            _enumerator.OnChanded -= UpdateState;
            _enumerator.OnEnd -= Close;
            
            Log.Debug("DialogViewer disposed");
            
            base.Dispose();
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