using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IPlayerAudioModelObserver: IAudioModelObserver
    {
        AudioClip JumpAudioClip { get; }
        AudioClip RespawnAudioClip { get; }
        AudioClip DamageAudioClip { get; }
    }
    
    [Serializable]
    public class PlayerAudioModel : InnerModel, IPlayerAudioModelObserver
    {
        [field: SerializeField] public AudioClip JumpAudioClip { get; private set; }
        [field: SerializeField] public AudioClip RespawnAudioClip { get; private set; }
        [field: SerializeField] public AudioClip DamageAudioClip { get; private set; }
        public event Action<AudioClip> OnPlayOneShot;

        public void PlayOneShot(AudioClip clip)
        {
            OnPlayOneShot?.Invoke(clip);
        }
    }
}