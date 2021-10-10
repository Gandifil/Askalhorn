using System;
using Askalhorn.Combat;
using Askalhorn.Common;
using Askalhorn.Dialogs;
using Askalhorn.Map;
using Askalhorn.Map.Local;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Builders
{
    internal class CharacterBuilder: IGameObjectBuilder
    {
        public string Name { get; set; }

        [JsonConstructor]
        public CharacterBuilder(string name)
        {
            Name = name;
        }
        
        public IGameObject Build(Position position)
        {
            var prototype = Storage.Content.Load<CharacterTypeInformation>("characters/" + Name);
            var character = new Character()
            {
                Name = prototype.Name,
                Position = position,
                Renderer = prototype.Renderer,
                Controller = prototype.Controller,
                Level = new Cultivation(prototype.Level),
                Dialog = string.IsNullOrEmpty(prototype.Dialog)
                    ? null
                    : Storage.Content.Load<Dialog>("dialogs/" + prototype.Dialog),
            };

            if (prototype.Loot is not null)
            {
                prototype.Loot.Fill(new Random(), character.Bag);
            }

            return character;
        }
    }
}