using Askalhorn.Common.Control;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    internal class Character: ICharacter
    {
        public string Name => "Test";
        public IObservedParameter HP { get;  set;}
        public IObservedParameter MaxHP { get;  set; }

        public IController Controller { get; set; }
        public Texture2D Texture { get; set; }

        IPosition ICharacter.Position => Position;
        public Position Position { get; set; }
    }
}