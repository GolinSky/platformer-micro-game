using Mario.Entities.Base;
using Mario.Services;
using UnityEngine;

namespace Mario.Entities.Music
{
    public class MusicView : View<IMusicModelObserver>
    {
        [SerializeField] private AudioSource audioSource;
        
        protected override void OnInitialize()
        {
            UpdateSettings(Model.AudioSettings);
            PlayClip(Model.MusicClip);
            Model.OnClipPlayed += PlayClip;
            Model.OnSettingsChanged += UpdateSettings;
        }

        protected override void OnDispose()
        {
            Model.OnClipPlayed -= PlayClip;
        }
        
        private void PlayClip(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        
        private void UpdateSettings(IAudioSettings audioSettings)
        {
            audioSource.mute = audioSettings.IsMute;
            audioSource.pitch = audioSettings.Speed;
        }
    }
}