using LightWeightFramework.Components.ViewComponents;
using Mario.Components.Health;
using UnityEngine;

namespace Mario.Components.Audio
{
    public class EnemyAudioViewComponent : ViewComponent<IEnemyAudioModelObserver>
    {
        [SerializeField] private AudioSource audioSource;

        private IHealthModelObserver healthModelObserver;

        protected override void OnInit()
        {
            base.OnInit();
            healthModelObserver = ModelObserver.GetModelObserver<IHealthModelObserver>();
            healthModelObserver.OnDied += PlayDieClip;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            healthModelObserver.OnDied -= PlayDieClip;
        }
        
        private void PlayDieClip()
        {
            PlayClip(Model.DamageAudioClip);
        }

        private void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}