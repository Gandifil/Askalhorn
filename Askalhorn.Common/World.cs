using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Askalhorn.Common.Control;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Geography.Local.Builds;
using Askalhorn.Common.Geography.Local.Generators;
using Askalhorn.Common.Geography.Local.Spawners;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics.Utils;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using Serilog;

namespace Askalhorn.Common
{
    public class World
    {
        public ILocation Location { get; protected set; }

        public IEnumerable<ICharacter> Characters => _characters;

        private List<Character> _characters;

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
        /// Create new world.
        /// </summary>
        public World()
        {
            Instance = this;
            
            _characters = new List<Character>
            {
                new()
                {
                    Texture = Storage.Content.Load<Texture2D>("images/mage"),
                    Controller = playerController,
                }
            };
            SetLocation(LocationPipeline.Templates[0], new Position(1, 1));
        }

        public World(string filepath)
        {
            throw new NotImplementedException();
        }

        internal void Add(Character character)
        {
            _characters.Add(character);
        }

        public ICharacter Player => Characters.First();
        public event Action<IBag> OnOpenBag;
        public event Action OnTurn;
        public event Action OnChangeLocation;

        internal void OpenBag(IBag bag)
        {
            OnOpenBag?.Invoke(bag);
        }

        internal void SetLocation(LocationPipeline pipeline, Position position)
        {
            var player = _characters[0];
            player.Position = position;
            _characters.Clear();
            _characters.Add(player);
            Location = pipeline.Run(position);
            OnChangeLocation?.Invoke();
        }

        internal void Turn()
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