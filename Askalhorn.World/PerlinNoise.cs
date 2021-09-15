using Askalhorn.Math;

namespace Askalhorn.World
{
    public class PerlinNoise : IMapProvider
    {
        private uint octaves;

        private readonly uint width;
        
        private readonly uint height;

        public PerlinNoise(uint octaves, uint width, uint height)
        {
            this.octaves = octaves;
            this.width = width;
            this.height = height;
        }

        public Map Get()
        {
            var levelMap = new FloatMapBuilder(width, height);
            levelMap.AddPerlinNoise(5f, 1f);
            levelMap.AddPerlinNoise(10f, .5f);
            levelMap.AddPerlinNoise(1f, .5f, -1);
            levelMap.ReduceBoundary();
            
            var tempMap = new FloatMapBuilder(width, height);
            tempMap.AddPerlinNoise(10f, 0.5f);
            tempMap.AddPerlinNoise(5f, 1f);
            tempMap.AddPerlinNoise(2f, 2f);
            
            var hydroMap = new FloatMapBuilder(width, height);
            hydroMap.AddPerlinNoise(5f, 1f);
            
            
            var result = new Map(width, height);
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {   
                result.Cells[x, y] = new Cell()
                {
                    Level = levelMap[x, y],
                    Biome = GenBiome(levelMap[x, y], tempMap[x, y], hydroMap[x, y]),
                    Temperature = tempMap[x, y],
                    Hydro = hydroMap[x, y],
                };
            }

            return result;
            
        }

        public IBiome GenBiome(float level, float temp, float hydro)
        {
            if (level < -0.1f)
                return Biome.Repository[0];
            if (level < 0f)
                return Biome.Repository[1];
            if (level < 0.05f)
                return Biome.Repository[2];
            if (temp + hydro > 0.3f)
                return Biome.Repository[4];
            else
                return Biome.Repository[3];
        }
    }
}
