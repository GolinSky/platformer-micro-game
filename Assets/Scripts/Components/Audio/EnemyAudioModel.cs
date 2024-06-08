using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IEnemyAudioModelObserver: IAudioModelObserver 
    {
        AudioClip DamageAudioClip { get; }
    }
    
    [Serializable]
    public class EnemyAudioModel:InnerModel, IEnemyAudioModelObserver
    {
        [field: SerializeField] public AudioClip DamageAudioClip { get; private set; }

        public event Action<AudioClip> OnPlayOneShot;

        public void PlayOneShot(AudioClip clip)
        {
            OnPlayOneShot?.Invoke(clip);
        }
    }
}