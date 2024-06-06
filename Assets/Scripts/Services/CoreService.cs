using LightWeightFramework.Components.Service;
using Mario.Entities.Player;
using Mario.Zenject.Scene;
using UnityEngine;
using Zenject;

namespace Mario.Services
{
    public class CoreService: Service, IInitializable
    {
        private readonly PlayerFacade playerFacade;
        private PlayerView playerView;

        [Inject(Id = TransformInjectKeys.SpawnPoint)]
        private Transform SpawnPoint { get; }
        
        public CoreService(PlayerFacade playerFacade)
        {
            this.playerFacade = playerFacade;
        }

        public void Initialize()
        {
            playerView = playerFacade.Create(SpawnPoint.position);
        }
    }
}