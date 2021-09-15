using Askalhorn.Common;

namespace Askalhorn.Map
{
    public class LocationInfo
    {
        public string PipelineName { get; set; }

        public int Seed { get; set; }

        public int[] Args { get; set; } = new int[0];

        public Location Generate(uint placeIndex)
        {
            var pipeline = Storage.Content.Load<LocationPipeline>("locations/" + PipelineName);
            return pipeline.Run(Seed, Args, placeIndex);
        }
    }
}