using Microsoft.Xna.Framework;

namespace Askalhorn.World
{
    public interface IBiome
    {
        string Name { get; }
        
        Color Color { get; }
    }

    public class Biome: IBiome
    {
        public string Name { get; set; }

        public Color Color { get; set; }


        // private static readonly Dictionary<Cell.WeatherType, Color> WeatherColors = new Dictionary<Cell.WeatherType, Color>
        // {
        //     {Cell.WeatherType.Forest, Color.DarkGreen },
        //     {Cell.WeatherType.Grassland, Color.LightGreen },
        //     {Cell.WeatherType.Jungle, Color.ForestGreen },
        //     {Cell.WeatherType.Tundra, Color.White },
        // };
        public static Biome[] Repository { get; } = new Biome[]
        {
            new Biome
            {
                Name = "Sea",
                Color = Color.DarkBlue,
            },
            new Biome
            {
                Name = "Coast",
                Color = Color.Blue,
            },
            new Biome
            {
                Name = "Desert",
                Color = Color.Yellow,
            },
            new Biome
            {
                Name = "Grassland",
                Color = Color.LightGreen,
            },
            new Biome
            {
                Name = "Forest",
                Color = Color.DarkGreen,
            },
        };
    }
}