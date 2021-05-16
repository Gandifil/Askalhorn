using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Control;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace Askalhorn.Common
{
    public class World
    {
        public ILocation Location { get; protected set; }

        public IEnumerable<ICharacter> Characters { get; protected set; } = new List<ICharacter>();

        public readonly BufferController playerController = new BufferController();
        
        /// <summary>
        /// Create world.
        /// </summary>
        public World()
        {
            Location = new TiledMapLocation("start");
            
            Characters = new List<ICharacter>
            {
                new StaticCharacter()
                {
                    Texture = Storage.Content.Load<Texture2D>("images/mage"),
                    Position = new Position(0, 0),
                    Controller = playerController,
                }
            };
        }

        public void Turn()
        {
            Log.Information("Run moves from all Controllers.");

            foreach (var character in Characters)
            foreach (var move in character.Controller.Moves)
                move.Make(this, character);
        }
    }
}