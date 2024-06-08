using System;
using UnityEngine;
using LightWeightFramework.Model;
using Mario.Services;

namespace Mario.Entities.Music
{
    public interface IMusicModelObserver : IModelObserver
    { 
        event Action<AudioClip> OnClipPlayed;
        event Action<IAudioSettings> OnSettingsChanged;//todo: IS -> audio model
        AudioClip MusicClip { get; }
    }

    [CreateAssetMenu(fileName = "MusicModel", menuName = "Model/MusicModel")]
    public class MusicModel : Model, IMusicModelObserver
    {
        public event Action<IAudioSettings> OnSettingsChanged;
        public event Action<AudioClip> OnClipPlayed;

        [field:SerializeField] public AudioClip MusicClip { get; private set; }
        
        public void ApplySettings(IAudioSettings audioSettings)
        {
            OnSettingsChanged?.Invoke(audioSettings);
        }
        
        public void PlayClip(AudioClip clip)
        {
            OnClipPlayed?.Invoke(clip);
        }
    }
}