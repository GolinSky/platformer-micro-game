using Mario.Services.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Mario.Entities.Ui.LobbyMenu
{
    public class LobbyMenuUi: Base.Ui
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        
        [Inject]
        private ILoadingCommand LoadingCommand { get; }

        public override void Initialize()
        {
            base.Initialize();
            playButton.onClick.AddListener(LoadingCommand.LoadCoreScene);
            exitButton.onClick.AddListener(Application.Quit);
        }

        public override void LateDispose()
        {
            base.LateDispose();
            playButton.onClick.RemoveListener(LoadingCommand.LoadCoreScene);
            exitButton.onClick.RemoveListener(Application.Quit);
        }
    }
}