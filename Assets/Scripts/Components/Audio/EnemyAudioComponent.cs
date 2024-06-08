using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Components.Health;
using UnityEngine;
using Zenject;

namespace Mario.Components.Audio
{
    public class EnemyAudioComponent : Component<EnemyAudioModel>, IInitializable, ILateDisposable
    {
        private readonly IHealthModelObserver healthModelObserver;

        public EnemyAudioComponent(IModel rootModel) : base(rootModel)
        {
            healthModelObserver = rootModel.GetModelObserver<IHealthModelObserver>();
        }
        
        public void Initialize()
        {
            healthModelObserver.OnDied += PlayDieClip;
        }

        public void LateDispose()
        {
            healthModelObserver.OnDied -= PlayDieClip;
        }
        
        private void PlayDieClip()
        {
            PlayClip(Model.DamageAudioClip);
        }

        private void PlayClip(AudioClip clip)
        {
            Model.PlayOneShot(clip);
        }
    }
}