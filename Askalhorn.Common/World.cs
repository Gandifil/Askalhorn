using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Askalhorn.Common.Control;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Geography.Local.Generators;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics.Utils;
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
        
        public static World Instance { get; private set; }

        internal Character Find(IPosition position)
        {
            foreach (var character in _characters)
                if (character.Position.Point == position.Point)
                    return character;

            return null;
        }
        
        /// <summary>
        /// Create world.
        /// </summary>
        public World()
        {
            Instance = this;
            var locationGenerator = new SimpleGenerator();
            Location = locationGenerator.Location;//new TiledMapLocation("start");
            
            _characters = new List<Character>
            {
                new Character()
                {
                    Texture = Storage.Content.Load<Texture2D>("images/mage"),
                    Position = new Position(2, 2),
                    Controller = playerController,
                },
                new Character()
                {
                    Texture = Storage.Content.Load<Texture2D>("images/mage2"),
                    Position = new Position(3, 3),
                },
            };

            _characters[1].Controller = new RandomMovementController(_characters[1]);
            _characters[0].Level.AddEnergy(1000);
        }

        public ICharacter Player => Characters.First();
        public event Action<IBag> OnOpenBag;
        public event Action OnTurn;

        internal void OpenBag(IBag bag)
        {
            OnOpenBag?.Invoke(bag);
        }

        public void Turn()
        {
            Log.Debug("Run moves from all Controllers.");

            foreach (var character in _characters)
            foreach (var move in character.Controller.Moves)
                move.Make(character);
            foreach (var character in _characters)
                character.Turn();
            _characters.RemoveAll(x => x.HP.Current < 1);
            OnTurn?.Invoke();
        }
    }
}