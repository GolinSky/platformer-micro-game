using System;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IEnemyAudioModelObserver: IAudioModelObserver 
    {
        AudioClip DamageAudioClip { get; }
    }
    
    [Serializable]
    public class EnemyAudioModel:BaseAudioModel, IEnemyAudioModelObserver
    {
        [field: SerializeField] public AudioClip DamageAudioClip { get; private set; }
    }
}