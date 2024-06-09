using Mario.Components.Audio;
using Mario.Components.Health;
using Mario.Components.Movement;
using Mario.Zenject.Extensions;
using Mario.Zenject.GameObjectInstallers;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Player
{
    public class PlayerInstaller: DynamicViewInstaller<PlayerController, PlayerModel, PlayerView>
    {
        private Vector3 startPosition;

        [Inject]
        private SceneContext SceneContext { get; }
        
        [Inject]
        public void Construct(Vector3 startPosition)
        {
            this.startPosition = startPosition;
        }

        protected override void BindParameters()
        {
            base.BindParameters();
            Container.BindEntity(startPosition);
            Container.BindEntity(EntityType.Player);
        }

        protected override void BindComponents()
        {
            base.BindComponents();
            Container
                .BindInterfaces<PlayerMovementComponent>()
                .BindInterfaces<HealthComponent>()
                .BindInterfaces<PlayerAudioComponent>();

        }

        protected override void BindModel()
        {
            base.BindModel();
            SceneContext
                .Container.Bind<IPlayerModelObserver>()
                .FromMethod(()=> Container.Resolve<IPlayerModelObserver>());
        }
    }
}