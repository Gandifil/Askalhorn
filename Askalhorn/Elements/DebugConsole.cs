using Askalhorn.Common;
using Askalhorn.Common.Localization;
using Askalhorn.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Font;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.Elements
{
    /// <summary>
    /// A gui element which represent debug develop console. It has a box for debug messages and a input field for writing command.
    /// </summary>
    public class DebugConsole: FixPanel
    {
        private const string UI_NAME = "DebugConsole";

        public static LineStorage LineStorage = new LineStorage();

        private const float ELEMENT_WIDTH = .95f;
        private const string COMMAND_COLOR = "Purple";
        private const int VERTICAL_SPACE = 3;
        
        private readonly FixPanel _output;
        private readonly TextField _input;
        
        public DebugConsole(Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            _output = new FixPanel(Anchor.TopCenter, 1f, 0.9f,true);
            AddChild(_output);

            AddChild(new Paragraph(Anchor.BottomLeft, 1, "Print command:", true));
            _input = new TextField(Anchor.BottomRight, new Vector2(.9f, 0.1f));
            _input.OnTextInput += (element, key, character) =>
            {
                if (key == Keys.Enter)
                    EnterCommand();
            };
            AddChild(_input);

            foreach (var line in LineStorage.Logs)
                Write(line);
            LineStorage.OnWrited += Write;
        }

        private void EnterCommand()
        {
            var command = _input.Text;
            _input.RemoveText(0, command.Length);
            
            LineStorage.Write((">>>  " + command).WithColor(COMMAND_COLOR));
        }

        public override void Dispose()
        {
            base.Dispose();
            
            LineStorage.OnWrited -= Write;
        }

        private void Write(string line)
        {
            _output.AddChild(new Paragraph(Anchor.AutoLeft, _output.Area.Width - 50, line)
            {
                RegularFont = new StyleProp<GenericFont>(new GenericSpriteFont(Storage.Content.Load<SpriteFont>("fonts/GameLogsFont"))),
            });
            _output.AddChild(new VerticalSpace(VERTICAL_SPACE));
            _output.ScrollBar.ForceUpdateArea();
            _output.ScrollBar.CurrentValue = _output.ScrollBar.MaxValue - 1;
        }

        public static bool IsExist => AskalhornGame.Instance.UiSystem.Get(DebugConsole.UI_NAME) is not null;

        public static void Toggle()
        {
            if (IsExist)
                AskalhornGame.Instance.UiSystem.Remove(UI_NAME);
            else
                AskalhornGame.Instance.UiSystem.Add(UI_NAME, new DebugConsole(Anchor.TopCenter, ELEMENT_WIDTH, 0.5f));
        }
    }
}