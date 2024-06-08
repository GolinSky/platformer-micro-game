using LightWeightFramework.Components.Service;

namespace Mario.Services
{
    public interface IAudioCommand
    {
        void MuteSound(bool isMuted);
        void MuteMusic(bool isMuted);

    }
    public class AudioService: Service, IAudioCommand
    {
        public void MuteSound(bool isMuted)
        {
            
        }

        public void MuteMusic(bool isMuted)
        {
            
        }
    }
}