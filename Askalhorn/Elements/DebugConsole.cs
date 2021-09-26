using System;
using System.IO;
using System.Linq;
using System.Xml;
using Askalhorn.Common;
using Askalhorn.Core;
using Askalhorn.Logging;
using Askalhorn.Text;
using Askalhorn.UI;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Font;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended.Input.InputListeners;

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

        public Element InputField => _input;
        
        public DebugConsole(Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            _output = new FixPanel(Anchor.TopCenter, 1f, 0.9f,true);
            AddChild(_output);

            AddChild(new Paragraph(Anchor.BottomLeft, 1, "Print command:", true));
            _input = new TextField(Anchor.BottomRight, new Vector2(.9f, 0.1f));
            AddChild(_input);

            foreach (var line in LineStorage.Logs)
                Write(line);
            LineStorage.OnWrited += Write;
            InputListeners.Input.KeyboardListener.Push(new NumericKeyboardListener());
            InputListeners.Input.MouseListener.Push(new MouseListener());
            InputListeners.Keyboard.KeyReleased += OnKeyReleased;
            ScrollToEnd();
        }

        private void OnKeyReleased(object? sender, KeyboardEventArgs args)
        {
            if (args.Key == Keys.OemTilde)
                this.Close();
            
            if (args.Key == Keys.Enter)
                EnterCommand();

            if (args.Key == Keys.Up)
            {
                try
                {
                    var commandsHistory = File.ReadAllLines("console.txt");
                    if (_historyIndex >= commandsHistory.Length)
                        _historyIndex = 0;
                    _input.SetText(commandsHistory[commandsHistory.Length - 1 - _historyIndex]);
                    _historyIndex++;
                }
                catch (Exception e)
                {
                }
            }
            else
                _historyIndex = 0;
        }

        public override void Dispose()
        {
            InputListeners.Keyboard.KeyReleased -= OnKeyReleased;
            InputListeners.Input.KeyboardListener.Pop();
            InputListeners.Input.MouseListener.Pop();
            LineStorage.OnWrited -= Write;
            
            base.Dispose();
        }

        private int _historyIndex = 0;

        private void EnterCommand()
        {
            var command = _input.Text;

            if (string.IsNullOrEmpty(command))
                return;
            
            _input.RemoveText(0, command.Length);
            
            using (var sw = File.AppendText("console.txt"))
            {
                sw.WriteLine(command);
            }	
            LineStorage.Write((">>>  " + command).WithColor(COMMAND_COLOR));
            GameProcess.Instance.RunConsoleCommand(command);
        }

        private void Write(string line)
        {
            _output.AddChild(new Paragraph(Anchor.AutoLeft, 1, line, true)
            {
                RegularFont = new StyleProp<GenericFont>(new GenericSpriteFont(Storage.Content.Load<SpriteFont>("fonts/GameLogsFont"))),
            });
            ScrollToEnd();
        }

        private void ScrollToEnd()
        {
            _output.ScrollBar.ForceUpdateArea();
            _output.ScrollBar.CurrentValue = _output.ScrollBar.MaxValue;
        }

        public static bool IsExist => AskalhornGame.Instance.UiSystem.Get(DebugConsole.UI_NAME) is not null;

        public static void Toggle()
        {
            if (IsExist)
                AskalhornGame.Instance.UiSystem.Remove(UI_NAME);
            else
                Open();
        }

        public static void Open()
        {
            var console = new DebugConsole(Anchor.TopCenter, ELEMENT_WIDTH, 0.5f);
            AskalhornGame.Instance.UiSystem.Add(UI_NAME, console)
                .SelectElement(console._input, true);
        }
    }
}