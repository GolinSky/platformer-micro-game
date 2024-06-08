using System;
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
    public class PlayerAudioModel : BaseAudioModel, IPlayerAudioModelObserver
    {
        [field: SerializeField] public AudioClip JumpAudioClip { get; private set; }
        [field: SerializeField] public AudioClip RespawnAudioClip { get; private set; }
        [field: SerializeField] public AudioClip DamageAudioClip { get; private set; }
    
    }
}