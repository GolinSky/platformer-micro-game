using LightWeightFramework.Model;
using Mario.Components.Health;
using Mario.Services;
using Zenject;

namespace Mario.Components.Audio
{
    public class EnemyAudioComponent : BaseAudioComponent<EnemyAudioModel>, IInitializable, ILateDisposable
    {
        private readonly IHealthModelObserver healthModelObserver;

        public EnemyAudioComponent(IModel rootModel, IAudioService audioService) : base(rootModel, audioService)
        {
            healthModelObserver = rootModel.GetModelObserver<IHealthModelObserver>();
        }
        
        protected override void OnInit()
        {
            healthModelObserver.OnDied += PlayDieClip;
        }

        protected override void OnRelease()
        {
            healthModelObserver.OnDied -= PlayDieClip;
        }
        
        private void PlayDieClip()
        {
            PlayClip(Model.DamageAudioClip);
        }
    }
}