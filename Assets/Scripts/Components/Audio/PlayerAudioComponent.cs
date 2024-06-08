using LightWeightFramework.Model;
using Mario.Components.Health;
using Mario.Components.Movement;
using Mario.Services;

namespace Mario.Components.Audio
{
    public class PlayerAudioComponent: BaseAudioComponent<PlayerAudioModel>
    {
        private readonly IHealthModelObserver healthModelObserver;
        private readonly IMovementModelObserver movementModelObserver;
        
        public PlayerAudioComponent(IModel rootModel, IAudioService audioService) : base(rootModel, audioService)
        {
            healthModelObserver = rootModel.GetModelObserver<IHealthModelObserver>();
            movementModelObserver = rootModel.GetModelObserver<IMovementModelObserver>();
        }
        
        protected override void OnInit()
        {
            healthModelObserver.OnDied += PlayDieClip;
            healthModelObserver.OnRespawn += PlayRespawnClip;
            movementModelObserver.OnJumped += PlayJumpClip;
        }

        protected override void OnRelease()
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
    }
}