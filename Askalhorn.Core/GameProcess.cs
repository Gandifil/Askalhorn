﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Askalhorn.Characters;
using Askalhorn.Characters.Control;
using Askalhorn.Common;
using Askalhorn.Map;
using Askalhorn.Map.Local;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Core
{
    public class GameProcess: TurnRunner
    {
        public override IReadOnlyCollection<Character> Characters =>
            Location.Current.Location.GameObjects.Select(x => x as Character).Where(x => x is not null).ToList();

        public static GameProcess Instance => TurnRunner.Instance as GameProcess;

        public IPlayer Player => Characters.First() as IPlayer;
        
        public BufferController PlayerController => (Characters.First() as Player).Controller as BufferController;
        
        /// <summary>
        /// Create new world and start new game.
        /// </summary>
        public GameProcess()
        {
            TurnRunner.Instance = this;
            
            Location.Current.Change(
                new Player()
                {
                },
                new LocationInfo
                {
                    PipelineName = "templeOutdoors",
                    Label = "start",
                    Seed = 10,
                });
        }

        /// <summary>
        /// Load game from save.
        /// </summary>
        /// <param name="filename">File name</param>
        public GameProcess(string filename) 
        {
            TurnRunner.Instance = this;
            
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            };
            
            using (var file = new StreamReader(filename + ".json"))
            {
                var state = JsonConvert.DeserializeObject<StateFile>(file.ReadToEnd(), settings);
                
                Location.Current.Change(state);
            }
        }

        public void Save(string filename)
        {
            using (var file = new StreamWriter(filename + ".json"))
            {
                file.Write(JsonConvert.SerializeObject(Location.Current.StateFile, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));
            }
        }

        public override void Turn()
        {
            base.Turn();
            
            InvokeOnTurned();
        }

        public void RunConsoleCommand(string line)
        {
#if DEBUG
            try
            {
                if (string.IsNullOrEmpty(line))
                    throw new ArgumentNullException(nameof(line));

                var words = line.Split(' ');
                new UniversalCommand().Run(null, words);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to run console command '{line}'", line);
            }
#endif
        }
    }
}