// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using Microsoft.Xna.Framework.Graphics;
// using Newtonsoft.Json;
// using Serilog;
// using JsonSerializer = System.Text.Json.JsonSerializer;
//
// namespace Askalhorn.Common
// {
//     public class World
//     {
//         internal class Info
//         {
//             public LocationInfo Location { get; set; }
//
//             public List<Character> Characters { get; set; } = new();
//
//             public List<Bag> Bags { get; set; } = new List<Bag>();
//         }
//
//         internal Info info = new();
//
//         public BufferController playerController => (BufferController) _characters[0].Controller;
//         
//         public static World Instance { get; private set; }
//
//         public Character Find(IPosition position)
//         {
//             foreach (var character in _characters)
//                 if (character.Position.Point == position.Point)
//                     return character;
//
//             return null;
//         }
//
//         public Character FindNear(IPosition position)
//         {
//             foreach (var character in _characters)
//                 if (character.Position.Point != position.Point && character.Position.IsInside(position, 1.5f))
//                     return character;
//
//             return null;
//         }
//         
//         /// <summary>
//         /// Create new world and start new game.
//         /// </summary>
//         public World()  
//         {
//             Instance = this;
//             
//             Add(new Player()
//             {
//                 Position = new Position(1, 1),
//             });
//             
//             SetLocation(
//                 new LocationInfo
//                 {
//                     PipelineName = "start",
//                     Seed = 10,
//                 }, 0);
//         }
//
//         /// <summary>
//         /// Load game from save.
//         /// </summary>
//         /// <param name="filename">File name</param>
//         public World(string filename) 
//         {
//             Instance = this;
//             
//             JsonSerializerSettings settings = new JsonSerializerSettings
//             {
//                 TypeNameHandling = TypeNameHandling.All
//             };
//             
//             using (var file = new StreamReader(filename + ".json"))
//             {
//                 info = JsonConvert.DeserializeObject<Info>(file.ReadToEnd(), settings);
//                 _location = info.Location.Generate(0);
//             }
//         }
//
//         public void Save(string filename)
//         {
//             using (var file = new StreamWriter(filename + ".json"))
//             {
//                 file.Write(JsonConvert.SerializeObject(info, new JsonSerializerSettings
//                 {
//                     TypeNameHandling = TypeNameHandling.All
//                 }));
//             }
//         }
//
//         internal void Add(Character character)
//         {
//             _characters.Add(character);
//         }
//
//         public IPlayer Player => (IPlayer)Characters.First();
//
//         internal void SetLocation(LocationInfo locationInfo, uint placeIndex)
//         {
//             info.Location = locationInfo;
//             
//             var player = _characters[0];
//             _characters.Clear();
//             _characters.Add(player);
//             
//             _location = locationInfo.Generate(0);
//             Location loc = (Location)Location;
//             player.Position = loc.Places[(int)placeIndex];
//             OnChangeLocation?.Invoke();
//         }
//
//         private void RemoveCharacter(Character ch)
//         {
//             if (!ch.Bag.IsEmpty)
//                 _location.AddBuild(new LootContainer(ch));
//         }
//
//         internal void Turn()
//         {
//             Log.Debug("Run moves from all Controllers.");
//
//             foreach (var character in _characters)
//             foreach (var move in character.Controller.Decide(character))
//             {
//                 move.Make(character);
//             }
//
//             var removing = _characters.Where(x => x.HP.Current < 1);
//             foreach (var character in removing)
//             {
//                 RemoveCharacter(character);
//             }
//             _characters.RemoveAll(x => x.HP.Current < 1);
//             foreach (var character in _characters)
//                 character.Turn();
//             OnTurn?.Invoke();
//         }
//     }
// }