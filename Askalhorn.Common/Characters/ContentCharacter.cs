using System;
using Askalhorn.Common.Control;
using Askalhorn.Common.Plot;

namespace Askalhorn.Common.Characters
{
    internal class ContentCharacter: Character
    {
        public ContentCharacter(string typeName)
        {
            var prototype = Storage.Content.Load<CharacterTypeInformation>("characters/" + typeName);
            Name = prototype.Name;
            Level.Base.Value = prototype.Level;
            Renderer = prototype.Renderer;
            Controller = prototype.Controller;

            if (prototype.Loot is not null)
            {
                prototype.Loot.Fill(new Random(), Bag);
            }

            if (!string.IsNullOrEmpty(prototype.Dialog))
            {
                Dialog = Storage.Content.Load<Dialog>("dialogs/" + prototype.Dialog);
            }
        }
        
    }
}