using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Dialogs
{
    public class DialogImpact: IImpact
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public Dialog Dialog { get; }

        public DialogImpact(Dialog dialog)
        {
            Dialog = dialog;
        }
        
        public void On(object target)
        {
            OnDialogOpened?.Invoke(new DialogEnumerator(target, Dialog));
        }

        public static event Action<DialogEnumerator> OnDialogOpened;
    }
}