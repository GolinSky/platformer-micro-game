using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Components.Health;
using Mario.Components.Movement;
using UnityEngine;
using Zenject;

namespace Mario.Components.Audio
{
    public class PlayerAudioComponent: Component<PlayerAudioModel>, IInitializable, ILateDisposable
    {
        private readonly IHealthModelObserver healthModelObserver;
        private readonly IMovementModelObserver movementModelObserver;
        
        public PlayerAudioComponent(IModel rootModel) : base(rootModel)
        {
            healthModelObserver = rootModel.GetModelObserver<IHealthModelObserver>();
            movementModelObserver = rootModel.GetModelObserver<IMovementModelObserver>();
        }
        
        public void Initialize()
        {
            healthModelObserver.OnDied += PlayDieClip;
            healthModelObserver.OnRespawn += PlayRespawnClip;
            movementModelObserver.OnJumped += PlayJumpClip;
        }

        public void LateDispose()
        {
            healthModelObserver.OnDied -= PlayDieClip;
            healthModelObserver.OnRespawn -= PlayRespawnClip;
            movementModelObserver.OnJumped -= PlayJumpClip;
        }

        private void PlayJumpClip()
        {
            PlayClip(Model.JumpAudioClip);
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
            Model.PlayOneShot(clip);
        }
    }
}