using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Geography.World.Render
{
    public class HydroMapRenderer: IMapRenderer
    {
        private readonly GraphicsDevice device;

        public HydroMapRenderer(GraphicsDevice device)
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
                data[y * map.Width + x] = Color.Lerp(Color.White, Color.Blue, map[x, y].Hydro + .5f); 
            }

            texture.SetData(data);
        }
    }
}