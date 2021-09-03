using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Plot;
using Askalhorn.Elements;
using Askalhorn.Elements.Inventory.Search;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class DialogTabComponent: IGameComponent, IDisposable
    {
        private static readonly string NAME = "dialog";
        private readonly Dialog _dialog;

        public DialogTabComponent(Dialog dialog)
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