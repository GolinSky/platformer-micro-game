using LightWeightFramework.Components.Service;
using Mario.Entities.Player;
using Mario.Entities.Ui.Base;
using Mario.Zenject.Scene;
using UnityEngine;
using Zenject;

namespace Mario.Services
{
    public interface ICoreGameCommand
    {
        void Exit();
        void EnterMenu();
        void ExitMenu();
    }
    public class CoreService: Service, IInitializable, ICoreGameCommand
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
            uiService.Show(UiType.CoreMenu);
            playerView = playerFacade.Create(SpawnPoint.position);
            uiService.Show(UiType.Player);
        }

        public void Exit()
        {
            uiService.Close(UiType.CoreMenu);
        }

        public void EnterMenu()
        {
            Time.timeScale = 0;
        }

        public void ExitMenu()
        {
            Time.timeScale = 1;
        }
    }
}