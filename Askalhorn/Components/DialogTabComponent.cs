using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Dialogs;
using Askalhorn.Elements;
using Askalhorn.UI;
using Askalhorn.UI.Dialogs;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class DialogTabComponent: IGameComponent, IDisposable
    {
        private static readonly string NAME = "dialog";
        private readonly DialogEnumerator _dialog;

        public DialogTabComponent(DialogEnumerator dialog)
        {
            _dialog = dialog;
        }
        
        public void Initialize()
        {
            AskalhornGame.Instance.UiSystem.Add(NAME, new  DialogViewer(_dialog, Anchor.BottomCenter, 
                0.95f, 0.2f));
        }

        public void Dispose()
        {
            AskalhornGame.Instance.UiSystem.Remove(NAME);
        }
    }
}