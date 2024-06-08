namespace Mario.Services
{
    public interface IAudioCommand
    {
        void MuteSound(bool isActive);
        void MuteMusic(bool isActive);
    }
}