using Askalhorn.Common.Plot;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Characters
{
    internal class Witch: Character
    {
        public Witch()
        {
            Name = "Ведьма";
            Texture = Storage.Content.Load<Texture2D>("images/mage2");
            var t = new DialogReader();
            Dialog = Storage.Content.Load<Dialog>("dialogs/witch");
        }
    }
}