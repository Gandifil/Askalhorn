using Askalhorn.Common.Geography.Local;

namespace Askalhorn.Common.Geography
{
    internal class LocationInfo
    {
        public string PipelineName { get; set; }

        public int Seed { get; set; }

        public Location Generate(IPosition position)
        {
            return LocationPipeline.Templates[PipelineName].Run(Seed, position);
        }
    }
}