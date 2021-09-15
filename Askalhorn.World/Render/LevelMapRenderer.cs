using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.World.Render
{
    public class LevelMapRenderer: IMapRenderer
    {
        private readonly GraphicsDevice device;

        public LevelMapRenderer(GraphicsDevice device)
        {
            this.device = device;
        }
        
        public void Draw(Map map, ref Texture2D texture)
        {
            if (texture != null && (texture.Width != map.Width || texture.Height != map.Height))
            {
                texture.Dispose();
                texture = null;
            }

            if (texture == null)
            {
                texture = new Texture2D(device, map.Width, map.Height, false, SurfaceFormat.Color);
            }

            var data = new Color[map.Width * map.Height];

            for (int y = 0; y < map.Height; y++)
            for (int x = 0; x < map.Width; x++)
            {
                float pc = (float)map.Cells[x, y].Level + .5f;
                data[y * map.Width + x] = new Color(pc, pc, pc, 1f); //WeatherColors[Cells[x, y].Weather];
            }

            texture.SetData(data);
        }
    }
}