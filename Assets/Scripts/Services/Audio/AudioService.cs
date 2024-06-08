using System.Collections.Generic;
using LightWeightFramework.Components.Service;
using UnityEngine;

namespace Mario.Services
{
    public interface IAudioService: IService
    {
        IAudioSettings SoundSettings { get; } 
        IAudioSettings MusicSettings { get; } 

        void AddSoundObserver(IAudioObserver observer);
        void RemoveSoundObserver(IAudioObserver observer);
        
        void AddMusicObserver(IAudioObserver observer);
        void RemoveMusicObserver(IAudioObserver observer);
        void PlayClipAtPoint(AudioClip clip, Vector3 position);
    }

    public class AudioService: Service, IAudioCommand, IAudioService
    {
        private readonly AudioSettings soundSettings;
        private readonly AudioSettings musicSettings;

        private readonly List<IAudioObserver> soundObservers = new List<IAudioObserver>();
        private readonly List<IAudioObserver> musicObservers = new List<IAudioObserver>();

        public IAudioSettings SoundSettings => soundSettings;
        public IAudioSettings MusicSettings => soundSettings;

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
            foreach (IAudioObserver musicObserver in musicObservers)
            {
                musicObserver.Update(musicSettings);
            }
        }

        public void AddSoundObserver(IAudioObserver observer)
        {
            soundObservers.Add(observer);
        }

        public void RemoveSoundObserver(IAudioObserver observer)
        {
            soundObservers.Remove(observer);
        }

        public void AddMusicObserver(IAudioObserver observer)
        {
            musicObservers.Add(observer);
        }

        public void RemoveMusicObserver(IAudioObserver observer)
        {
            musicObservers.Remove(observer);
        }

        public void PlayClipAtPoint(AudioClip clip, Vector3 position)
        {
            if(soundSettings.IsMute) return;
            
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}