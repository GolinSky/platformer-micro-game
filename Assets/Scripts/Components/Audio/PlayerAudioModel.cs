using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IPlayerAudioModelObserver: IModelObserver
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
    }
}