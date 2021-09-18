using System.Collections.Generic;

namespace Askalhorn.Map
{
    public class StateFile
    {
        public LocationInfo LocationInfo { get; set; }
        
        public List<IGameObject> Objects { get; set; }
    }
}