namespace Mario.Services
{
    public class AudioSettings : IAudioSettings
    {
        private const float DefaultSpeed = 1f;
        public bool IsMute { get; set; }
        public float Speed { get; set; }

        public AudioSettings()
        {
            IsMute = false;
            Speed = DefaultSpeed;
        }
    }
}