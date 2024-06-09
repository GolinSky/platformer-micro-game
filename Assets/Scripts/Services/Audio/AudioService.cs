using System.Collections.Generic;
using LightWeightFramework.Components.Service;
using Mario.Services.SaveData;
using UnityEngine;
using Zenject;

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

    public class AudioService: Service, IAudioCommand, IAudioService, IInitializable, ILateDisposable, IGameObserver
    {
        private readonly ICoreService coreService;
        private readonly ISaveDataService saveDataService;
        private readonly AudioSettings soundSettings;
        private readonly AudioSettings musicSettings;

        private readonly List<IAudioObserver> soundObservers = new List<IAudioObserver>();
        private readonly List<IAudioObserver> musicObservers = new List<IAudioObserver>();

        public IAudioSettings SoundSettings => soundSettings;
        public IAudioSettings MusicSettings => musicSettings;

        private string SaveDataKey => nameof(AudioService);

        public AudioService(ICoreService coreService, ISaveDataService saveDataService)
        {
            this.coreService = coreService;
            this.saveDataService = saveDataService;
            if(saveDataService.HasKey(SaveDataKey))
            {
                AudioDto audioDto = saveDataService.Get<AudioDto>(SaveDataKey);
                musicSettings = audioDto.MusicSettings;
                soundSettings = audioDto.SoundSettings;
            }
            else
            {
                soundSettings = new AudioSettings();
                musicSettings = new AudioSettings();
            }
        }
        
        public void Initialize()
        {
            coreService.AddObserver(this);
            UpdateSoundObservers();
            UpdateSoundObservers();
        }

        public void LateDispose()
        {
            coreService.RemoveObserver(this);
        }
        
        public void MuteSound(bool isActive)
        {
            soundSettings.IsMute = !isActive;
            UpdateSoundObservers();
        }
        
        public void MuteMusic(bool isActive)
        {
            musicSettings.IsMute = !isActive;
            UpdateMusicObservers();
        }

        private void UpdateMusicObservers()
        {
            foreach (IAudioObserver musicObserver in musicObservers)
            {
                musicObserver.Update(musicSettings);
            }
        }
        
        private void UpdateSoundObservers()
        {
            foreach (IAudioObserver soundObserver in soundObservers)
            {
                soundObserver.Update(soundSettings);
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

 

        public void Update(GameState state)
        {
            if (state == GameState.Exit)
            {
                AudioDto audioDto = new AudioDto(SaveDataKey, soundSettings, musicSettings);
                saveDataService.Save(audioDto);
            }
        }
    }
}