using System;
using System.Collections.Generic;
using Askalhorn.Components;
using Askalhorn.Elements;
using Askalhorn.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class ControlSettings: BackScreenBase
    {
        private class Item : IDisposable
        {
            public readonly Panel Panel;
            public readonly Button Button;
            public readonly Options.KeyActions KeyAction;
            
            public Dictionary<Options.KeyActions, string> KeyLabels { get; set; } = new()
            {
                {Options.KeyActions.TopLeft, "Вверх-влево"}, 
                {Options.KeyActions.TopRight, "Вверх-вправо"}, 
                {Options.KeyActions.BottomLeft, "Вниз-влево"}, 
                {Options.KeyActions.BottomRight, "Вниз-вправо"}, 
                {Options.KeyActions.Character,"Персонаж"}, 
                {Options.KeyActions.Inventory,"Инвентарь"}, 
                {Options.KeyActions.Abilities, "Способности"},  
                {Options.KeyActions.Use, "Использовать"}, 
                {Options.KeyActions.Pause, "Назад/Пауза"}, 
            };
            
            public Item(KeyValuePair<Options.KeyActions,Keys> key)
            {
                KeyAction = key.Key;
                Panel = new Panel(Anchor.AutoCenter, new Vector2(1, Menu.ELEMENT_HEIGHT), Vector2.Zero);
                Panel.AddChild(new Paragraph(Anchor.CenterLeft, 200, KeyLabels[key.Key]));
                Button = new Button(Anchor.CenterRight, new Vector2(0.5f, Menu.ELEMENT_HEIGHT),key.Value.ToString());
                Panel.AddChild(Button);
            }
            
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
        
        private readonly AskalhornGame _game;
        private readonly Menu _menu;
        private readonly Options _options;
        private bool IsWaitReleaseKey;
        private Item WaitingKeyAction;

        
        public ControlSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
            _menu = new Menu(game.UiSystem);
            _options = Settings.Configuration.Options;
        }

        public override void Initialize() 
        {
            var keyboardListener = new KeyboardListener();
            keyboardListener.KeyReleased += KeyRelease;
            _game.Components.Add(new InputListenerComponent(_game, keyboardListener));

            foreach (var key in _options.Keys)
            {
                var item = new Item(key);
                item.Button.OnPressed += _ =>
                {
                    if (!IsWaitReleaseKey)
                    {
                        IsWaitReleaseKey = true;
                        item.Button.Text.Text = "???";
                        WaitingKeyAction = item;
                    }
                };
                _menu.Add(item.Panel);
            }
            _menu.AddButton("Назад", Back);
            _menu.Initialize();
        }
        
        private void KeyRelease(object sender, KeyboardEventArgs e)
        {
            if (IsWaitReleaseKey)
            {
                WaitingKeyAction.Button.Text.Text = e.Key.ToString();
                IsWaitReleaseKey = false;
                _options.Keys[WaitingKeyAction.KeyAction] = e.Key;
                Settings.Configuration.Change();
            }
        }

        public override void Dispose()
        {
            _menu.Dispose();
            _game.Components.ClearWithDispose();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}