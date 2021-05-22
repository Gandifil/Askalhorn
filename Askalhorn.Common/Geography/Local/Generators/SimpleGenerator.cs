using Askalhorn.Common.Geography.Local.Builds;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Generators
{
    class SimpleGenerator: IManagedLocationGenerator
    {
        public ManagedLocation Location
        {
            get
            {
                var location = new ManagedLocation(50, 50);

                location.AddBuild(0, 0, new LocalTeleport()
                {
                    Shift = new Point(10, 10),
                });

                location.AddBuild(0, 5, new GlobalTeleport()
                {
                    Shift = new Point(20, 20),
                });
                
                return location;
            }
        }
    }
}