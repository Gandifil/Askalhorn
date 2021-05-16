using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Maths
{
    public class FloatMapBuilder
    {
        private float[,] data;

        private readonly uint width;
        
        private readonly uint height;

        public float this[int x, int y] => data[x, y];

        public FloatMapBuilder(uint width, uint height)
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
}