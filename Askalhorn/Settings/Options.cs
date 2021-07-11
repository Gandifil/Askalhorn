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
    }
}