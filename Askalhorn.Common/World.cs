using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Control;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Inventory.Items;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Askalhorn.Common
{
    public class World
    {
        internal class Info
        {
            public LocationInfo Location { get; set; }

            public List<Character> Characters { get; set; } = new();

            public List<Bag> Bags { get; set; } = new List<Bag>();
        }
        
        public ILocation Location { get; protected set; }

        internal Info info = new();

        public IReadOnlyCollection<ICharacter> Characters => info.Characters;

        private List<Character> _characters => info.Characters;

        public BufferController playerController => (BufferController) _characters[0].Controller;
        
        public static World Instance { get; private set; }

        internal Character Find(IPosition position)
        {
            foreach (var character in _characters)
                if (character.Position.Point == position.Point)
                    return character;

            return null;
        }
        
        /// <summary>
        /// Create new world and start new game.
        /// </summary>
        public World()  
        {
            Instance = this;
            
            Add(new Player()
            {
                Position = new Position(1, 1),
            });
            Player.Bag.Put(new PoisonPoition
            {
                Value = 10,
                TurnCount = 5,
            }, 3);
            
            SetLocation(
                new LocationInfo
                {
                    PipelineName = "start",
                    Seed = 10,
                }, 0);
        }

        /// <summary>
        /// Load game from save.
        /// </summary>
        /// <param name="filename">File name</param>
        public World(string filename) 
        {
            Instance = this;
            
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            
            using (var file = new StreamReader(filename + ".json"))
            {
                info = JsonConvert.DeserializeObject<Info>(file.ReadToEnd(), settings);
                Location = info.Location.Generate(0);
            }
        }

        public void Save(string filename)
        {
            using (var file = new StreamWriter(filename + ".json"))
            {
                file.Write(JsonConvert.SerializeObject(info, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));
            }
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

        internal void SetLocation(LocationInfo locationInfo, uint placeIndex)
        {
            info.Location = locationInfo;
            var player = _characters[0];
            _characters.Clear();
            Location = locationInfo.Generate(0);
            Location loc = (Location)Location;
            player.Position = loc.Places[(int)placeIndex];
            _characters.Add(player);
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