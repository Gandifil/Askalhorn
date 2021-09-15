using System;

namespace Askalhorn.Map
{
    public sealed class LocationKeeper
    {
        public Location Location { get; private set; }

        public event Action OnChange;

        public void Change(GameObject target, LocationInfo info, uint placeIndex)
        {
             Location = info.Generate(placeIndex);
             target.Position = Location.Places[(int)placeIndex];
             Location.Add(target);
             OnChange?.Invoke();
        }
    }
}