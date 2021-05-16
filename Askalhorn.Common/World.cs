using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    public class World
    {
        public ILocation Location { get; protected set; }

        public IEnumerable<ICharacter> Characters { get; protected set; } = new List<ICharacter>();

        public ICharacter player;
        
        /// <summary>
        /// Create world.
        /// </summary>
        public World()
        {
            Location = new TiledMapLocation("start");
            player = new StaticCharacter()
            {
                Texture = Storage.Content.Load<Texture2D>("images/mage"),
                Position = new Position(0, 0),
            };
            Characters = new List<ICharacter>
            {
                player
            };
        }
    }
}