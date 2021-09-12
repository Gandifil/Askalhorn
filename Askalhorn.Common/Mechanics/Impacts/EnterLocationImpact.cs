using Askalhorn.Common.Geography;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    public class EnterLocationImpact: IImpact
    {
        public string Description { get; }
        
        public TextureRegion2D TextureRegion => null;

        internal readonly LocationInfo Location;

        public readonly uint Place;

        [JsonConstructor]
        internal EnterLocationImpact(LocationInfo location, uint place = 0)
        {
            Location = location;
            Place = place;
        }
        
        public void On(Character character)
        {
            Log.Information("{Name} enter to another location", character.Name);

            World.Instance.SetLocation(Location, Place);
        }
    }
}