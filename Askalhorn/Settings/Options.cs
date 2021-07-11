using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Askalhorn.Settings
{
    public class Options
    {
        public bool IsFullScreen { get; set; } = false;
        
        // sound options
        public float CommonVolume { get; set; } = 1.0f;
        public float SongVolume { get; set; } = 1.0f;
        public float SoundVolume { get; set; } = 1.0f;
        public float RealSongVolume => CommonVolume * SongVolume;
        public float RealSoundVolume => CommonVolume * SoundVolume;

        public enum KeyActions
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Character,
            Inventory,
            Abilities,
            Use,
            Pause,
        }

        public Dictionary<KeyActions, Keys> Keys { get; set; } = new()
        {
            {KeyActions.TopLeft, Microsoft.Xna.Framework.Input.Keys.A}, 
            {KeyActions.TopRight, Microsoft.Xna.Framework.Input.Keys.W}, 
            {KeyActions.BottomLeft, Microsoft.Xna.Framework.Input.Keys.S}, 
            {KeyActions.BottomRight, Microsoft.Xna.Framework.Input.Keys.D}, 
            {KeyActions.Character, Microsoft.Xna.Framework.Input.Keys.C}, 
            {KeyActions.Inventory, Microsoft.Xna.Framework.Input.Keys.I}, 
            {KeyActions.Abilities, Microsoft.Xna.Framework.Input.Keys.B}, 
            {KeyActions.Use, Microsoft.Xna.Framework.Input.Keys.E}, 
            {KeyActions.Pause, Microsoft.Xna.Framework.Input.Keys.Escape}, 
        };
    }
}