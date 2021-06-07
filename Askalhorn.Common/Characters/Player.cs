using Askalhorn.Common.Control;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Characters
{
    internal class Player: Character
    {
        public Player()
        {
            Name = "Вася";
            Texture = Storage.Content.Load<Texture2D>("images/mage");
            Controller = new BufferController();
        }
    }
}