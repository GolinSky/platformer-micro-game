using System.Collections.Generic;
using LightWeightFramework.Components.Service;

namespace Mario.Services
{
    public interface IAudioCommand
    {
        void MuteSound(bool isActive);
        void MuteMusic(bool isActive);
    }

    public interface IAudioService: IService
    {
        IAudioSettings SoundSettings { get; } 

        void AddSoundObserver(IAudioObserver observer);
        void RemoveSoundObserver(IAudioObserver observer);
    }

    public class AudioService: Service, IAudioCommand, IAudioService
    {
        private AudioSettings soundSettings;
        private AudioSettings musicSettings;

        private List<IAudioObserver> soundObservers = new List<IAudioObserver>();

        public IAudioSettings SoundSettings => soundSettings;

        public AudioService()
        {
            soundSettings = new AudioSettings();
            musicSettings = new AudioSettings();
        }
        
        public void MuteSound(bool isActive)
        {
            soundSettings.IsMute = !isActive;
            foreach (IAudioObserver soundObserver in soundObservers)
            {
                soundObserver.Update(soundSettings);
            }
        }

        public void MuteMusic(bool isActive)
        {
            musicSettings.IsMute = !isActive;
        }

        public void AddSoundObserver(IAudioObserver observer)
        {
            soundObservers.Add(observer);
        }

        public void RemoveSoundObserver(IAudioObserver observer)
        {
            soundObservers.Remove(observer);
        }
    }
}