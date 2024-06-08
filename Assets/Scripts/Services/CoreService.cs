using LightWeightFramework.Components.Service;
using Mario.Entities.Player;
using Mario.Entities.Ui.Base;
using Mario.Zenject.Scene;
using UnityEngine;
using Zenject;

namespace Mario.Services
{
    public class CoreService: Service, IInitializable
    {
        private readonly PlayerFacade playerFacade;
        private readonly IUiService uiService;
        private PlayerView playerView;

        [Inject(Id = TransformInjectKeys.SpawnPoint)]
        private Transform SpawnPoint { get; }
        
        public CoreService(PlayerFacade playerFacade, IUiService uiService)
        {
            this.playerFacade = playerFacade;
            this.uiService = uiService;
        }

        public void Initialize()
        {
            playerView = playerFacade.Create(SpawnPoint.position);
            uiService.Show(UiType.Player);
        }
    }
}