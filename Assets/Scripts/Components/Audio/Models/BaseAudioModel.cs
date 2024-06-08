using System;
using LightWeightFramework.Model;
using Mario.Services;
using UnityEngine;

namespace Mario.Components.Audio
{
    public class BaseAudioModel: InnerModel, IAudioModelObserver
    {
        public event Action<IAudioSettings> OnSettingsChanged;
        public event Action<AudioClip> OnPlayOneShot;

        public void PlayOneShot(AudioClip clip)
        {
            OnPlayOneShot?.Invoke(clip);
        }

        public void ApplySettings(IAudioSettings audioSettings)
        {
            OnSettingsChanged?.Invoke(audioSettings);
        }
    }
}