using System;
using LightWeightFramework.Model;
using Mario.Services;
using UnityEngine;

namespace Mario.Components.Audio
{
    public interface IAudioModelObserver:IModelObserver
    {
        event Action<IAudioSettings> OnSettingsChanged;
        event Action<AudioClip> OnPlayOneShot;
    }
}