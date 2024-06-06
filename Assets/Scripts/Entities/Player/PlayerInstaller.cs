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
        public void Construct(Vector3 startPosition)
        {
            this.startPosition = startPosition;
        }

        protected override void BindParameters()
        {
            base.BindParameters();
            Container.BindEntity(startPosition);
        }
    }
}