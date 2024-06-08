using LightWeightFramework.Components.ViewComponents;
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
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            Model.OnPlayOneShot -= PlayOneShot;
        }

        private void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}