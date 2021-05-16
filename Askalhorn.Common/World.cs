﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        //public IEnumerable<ICharacter> Characters { get; protected set; } = new List<ICharacter>();

        public IEnumerable<ICharacter> Characters => _characters;

        private List<Character> _characters;// = new List<Character>();

        public readonly BufferController playerController = new BufferController();
        
        /// <summary>
        /// Create world.
        /// </summary>
        public World()
        {
            Location = new ManagedLocation();//new TiledMapLocation("start");
            
            _characters = new List<Character>
            {
                new Character()
                {
                    Texture = Storage.Content.Load<Texture2D>("images/mage"),
                    Position = new Position(0, 0),
                    Controller = playerController,
                },
                new Character()
                {
                    Texture = Storage.Content.Load<Texture2D>("images/mage2"),
                    Position = new Position(2, 2),
                    Controller = new RandomMovementController(),
                },
            };
        }

        public void Turn()
        {
            Log.Information("Run moves from all Controllers.");

            foreach (var character in _characters)
            foreach (var move in character.Controller.Moves)
                move.Make(this, character);
        }
    }
}