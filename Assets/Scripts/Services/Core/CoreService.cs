using System.Collections.Generic;
using LightWeightFramework.Components.Service;
using Mario.Entities.Player;
using Mario.Entities.Ui.Base;
using Mario.Services.SceneLoading;
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
        void WinGame();
    }

    public interface ICoreService: IService
    {
        void AddObserver(IGameObserver gameObserver);
        void RemoveObserver(IGameObserver gameObserver);
    }

    public class CoreService: Service, IInitializable, ICoreGameCommand, ICoreService
    {
        private readonly PlayerFacade playerFacade;
        private readonly IUiService uiService;
        private readonly ISceneService sceneService;
        private readonly List<IGameObserver> gameObservers = new List<IGameObserver>();
        private PlayerView playerView;
        

        [Inject(Id = TransformInjectKeys.SpawnPoint)]
        private Transform SpawnPoint { get; }
        
        public CoreService(PlayerFacade playerFacade, IUiService uiService, ISceneService sceneService)
        {
            this.playerFacade = playerFacade;
            this.uiService = uiService;
            this.sceneService = sceneService;
        }

        public void Initialize()
        {
            uiService.Show(UiType.CoreMenu);
            playerView = playerFacade.Create(SpawnPoint.position);
            uiService.Show(UiType.Player);
        }

        public void Exit()
        {
            UpdateState(GameState.Exit);
            uiService.Close(UiType.CoreMenu);
            sceneService.LoadScene(SceneType.Lobby);
        }

        public void EnterMenu()
        {
            Time.timeScale = 0;
            UpdateState(GameState.Pause);
        }

        public void ExitMenu()
        {
            Time.timeScale = 1;
            UpdateState(GameState.Play);
        }

        public void WinGame()
        {
            UpdateState(GameState.Win);
        }

        private void UpdateState(GameState state)
        {
            foreach (IGameObserver gameObserver in gameObservers)
            {
                gameObserver.Update(state);
            }
        }

        public void AddObserver(IGameObserver gameObserver)
        {
            gameObservers.Add(gameObserver);
        }

        public void RemoveObserver(IGameObserver gameObserver)
        {
            gameObservers.Remove(gameObserver);
        }
    }
}