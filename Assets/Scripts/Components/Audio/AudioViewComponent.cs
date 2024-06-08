using LightWeightFramework.Components.ViewComponents;
using Mario.Services;
using UnityEngine;

namespace Mario.Components.Audio
{
    public class AudioViewComponent:ViewComponent<IAudioModelObserver>
    {
        [SerializeField] private AudioSource audioSource;

        protected override void OnInit()
        {
            base.OnInit();
            Model.OnPlayOneShot += PlayOneShot;
            Model.OnSettingsChanged += UpdateSettings;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            Model.OnPlayOneShot -= PlayOneShot;
        }
        
        private void UpdateSettings(IAudioSettings audioSettings)
        {
            audioSource.mute = audioSettings.IsMute;
        }

        private void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}