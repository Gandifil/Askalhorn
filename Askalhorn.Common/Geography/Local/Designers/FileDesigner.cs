using System;

namespace Askalhorn.Common.Geography.Local.Designers
{
    internal class FileDesigner: ILocationDesigner
    {
        public string Name { get; set; } = "start";
        
        public Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map)
        {
            return new Location(Name);
        }
    }
}