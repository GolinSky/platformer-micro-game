namespace Mario.Services
{
    public interface IAudioSettings
    {
        bool IsMute { get; }
        float Speed { get; }
    }
}