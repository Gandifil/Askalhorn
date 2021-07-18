using Askalhorn.Common.Control;

namespace Askalhorn.Common.Characters
{
    internal class ContentCharacter: Character
    {
        public ContentCharacter(string typeName)
        {
            Controller = new AgressiveController
            {
                Parent = this,
            };
            
            var prototype = Storage.Content.Load<CharacterTypeInformation>("characters/" + typeName);
            Name = prototype.Name;
            Level.Base.Value = prototype.Level;
            Renderer = prototype.Renderer;
        }
        
    }
}