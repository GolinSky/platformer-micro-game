using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IEnemyAudioModelObserver: IModelObserver 
    {
        AudioClip DamageAudioClip { get; }
    }
    
    [Serializable]
    public class EnemyAudioModel:InnerModel, IEnemyAudioModelObserver
    {
        [field: SerializeField] public AudioClip DamageAudioClip { get; private set; }

    }
}