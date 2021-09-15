using System;
using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
using Serilog;

namespace Askalhorn.Map.Impacts
{
    public class EnterLocationImpact: IImpact
    {
        public string Description { get; }
        
        public TextureRegion2D TextureRegion => null;

        internal readonly LocationInfo NextLocation;

        public readonly uint Place;

        [JsonConstructor]
        public EnterLocationImpact(LocationInfo nextLocation, uint place = 0)
        {
            NextLocation = nextLocation;
            Place = place;
        }
        
        public void On(object target)
        {
            var gameObject = target as GameObject;
            if (gameObject is null)
                throw new ArgumentNullException(nameof(gameObject));

            //Log.Information<string>("{Name} enter to another location", character.Name);

            Location.Current.Change(gameObject, NextLocation, Place);
        }
    }
}