using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IAudioModelObserver:IModelObserver
    {
        event Action<AudioClip> OnPlayOneShot;
    }
}