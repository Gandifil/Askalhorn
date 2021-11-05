using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Map.Local;

namespace Askalhorn.Map
{
    public sealed class LocationKeeper
    {
        public Location Location { get; private set; }

        public LocationInfo Info { get; set; }
        
        public StateFile StateFile => new StateFile()
        {
            LocationInfo = Info,
            Objects = Location.GameObjects.Where(x => !x.IsStatic).ToList(),
        };

        public event Action OnChange;

        public void Change(GameObject target, LocationInfo info)
        {
            Info = info;
            Location = info.Generate(false);
            target.Position = Location.Labels[info.Label] as Position;
            Location.Add(target);
            OnChange?.Invoke();
        }

        public void Change(StateFile stateFile)
        {
            Info = stateFile.LocationInfo;
            Location = stateFile.LocationInfo.Generate(true);
            stateFile.Objects.Reverse();
            foreach (var obj in stateFile.Objects)
                Location.Add(obj);
            OnChange?.Invoke();
        }
    }
}