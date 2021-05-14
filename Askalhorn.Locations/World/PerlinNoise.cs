using Askalhorn.Locations.Maths;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Askalhorn.Locations.World
{
    public class MapBuilder
    {
        private float[,] data;

        private readonly uint width;
        
        private readonly uint height;

        public float this[int x, int y] => data[x, y];

        public MapBuilder(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            data = new float[width, height];
        }

        public void AddPerlinNoise(float frequency, float amplitude, float k = 1)
        {
            var noise = new PerlinNoise2D(frequency, amplitude);
            
            Parallel.For(0, width, (x) =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        data[x, y] += k * noise[(float) x / width, (float) y / height];
                    }
                }
            );
            
        }

        public void Add(float value)
        {
            Parallel.For(0, width, (x) =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        data[x, y] += value;
                    }
                }
            );
            
        }

        public void ReduceBoundary()
        {
            var center = new Vector2((float)width / 2, (float)height / 2);
            var len = center.Length() / 2.0f;
            
            Parallel.For(0, width, (x) =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        var radVec = (new Vector2(x, y) - center).Length();
                        data[x, y] -= radVec / len / 1.5f;
                    }
                }
            );
        }
    }
    
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
            var levelMap = new MapBuilder(width, height);
            levelMap.AddPerlinNoise(5f, 1f);
            levelMap.AddPerlinNoise(10f, .5f);
            levelMap.AddPerlinNoise(1f, .5f, -1);
            levelMap.ReduceBoundary();
            
            var tempMap = new MapBuilder(width, height);
            tempMap.AddPerlinNoise(10f, 0.5f);
            tempMap.AddPerlinNoise(5f, 1f);
            tempMap.AddPerlinNoise(2f, 2f);
            levelMap.Add(1f);
            
            var hydroMap = new MapBuilder(width, height);
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
