using Askalhorn.Render;

namespace Askalhorn.Map.Builds
{
    class LocationDoor: GlobalTeleport
    {
        public LocationDoor()
        {
            Renderer = new EmptyRenderer();
        }
    }
}