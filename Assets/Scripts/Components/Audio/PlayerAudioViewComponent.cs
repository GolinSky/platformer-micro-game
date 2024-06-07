using LightWeightFramework.Components.ViewComponents;
using Mario.Components.Health;
using Mario.Components.Movement;
using UnityEngine;

namespace Mario.Components.Audio
{
    public class PlayerAudioViewComponent: ViewComponent<IPlayerAudioModelObserver>
    {
        [SerializeField] private AudioSource audioSource;
        
        private IHealthModelObserver healthModelObserver;
        private IMovementModelObserver movementModelObserver;
        


        protected override void OnInit()
        {
            base.OnInit();
            healthModelObserver = ModelObserver.GetModelObserver<IHealthModelObserver>();
            movementModelObserver = ModelObserver.GetModelObserver<IMovementModelObserver>();
            
            healthModelObserver.OnDied += PlayDieClip;
            healthModelObserver.OnRespawn += PlayRespawnClip;
            movementModelObserver.OnJumped += PlayJumpClip;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            healthModelObserver.OnDied -= PlayDieClip;
            healthModelObserver.OnRespawn -= PlayRespawnClip;
            movementModelObserver.OnJumped -= PlayJumpClip;
        }

        private void PlayJumpClip()
        {
            audioSource.PlayOneShot(Model.JumpAudioClip);
        }

        private void PlayDieClip()
        {
            PlayClip(Model.DamageAudioClip);
        }

        private void PlayRespawnClip()
        {
            PlayClip(Model.RespawnAudioClip);
        }
        
        private void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}