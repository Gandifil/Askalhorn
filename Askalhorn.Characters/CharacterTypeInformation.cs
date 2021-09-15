using Askalhorn.Characters.Control;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Render;

namespace Askalhorn.Characters
{
    public class CharacterTypeInformation
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public TextureRenderer Renderer { get; set; }

        public string Dialog { get; set; }
        public IController Controller { get; set; } = new AgressiveController();

        public ILootChooser Loot { get; set; }
    }
    
    public class CharacterTypeInformationReader : PolymorphJsonReader<CharacterTypeInformation>
    {
    }
}