using Askalhorn.Common.Geography.Local;

namespace Askalhorn.Common.Geography
{
    internal class LocationInfo
    {
        public string PipelineName { get; set; }

        public int Seed { get; set; }

        public int[] Args { get; set; } = new int[0];

        public Location Generate(uint placeIndex)
        {
            return LocationPipeline.Templates[PipelineName].Run(Seed, Args, placeIndex);
        }
    }
}