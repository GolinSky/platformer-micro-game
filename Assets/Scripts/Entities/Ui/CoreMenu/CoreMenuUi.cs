using Mario.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Mario.Entities.Ui.CoreMenu
{
    public class CoreMenuUi: Base.Ui
    {
        [SerializeField] private Canvas innerCanvas;
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle musicToggle;

        [Inject]
        private ICoreGameCommand CoreGameCommand { get; }
        
        public override void Initialize()
        {
            base.Initialize();
            exitButton.onClick.AddListener(CoreGameCommand.Exit);
            openButton.onClick.AddListener(OpenMenu);
            closeButton.onClick.AddListener(CloseMenu);
            openButton.onClick.AddListener(CoreGameCommand.EnterMenu);
            closeButton.onClick.AddListener(CoreGameCommand.ExitMenu);
        }

        public override void LateDispose()
        {
            base.LateDispose();
            exitButton.onClick.RemoveListener(CoreGameCommand.Exit);
            openButton.onClick.RemoveListener(OpenMenu);
            closeButton.onClick.RemoveListener(CloseMenu);
            openButton.onClick.RemoveListener(CoreGameCommand.EnterMenu);
            closeButton.onClick.RemoveListener(CoreGameCommand.ExitMenu);
        }
        
        private void CloseMenu()
        {
            ActivateMenuCanvas(false);
        }
        
        private void OpenMenu()
        {
            ActivateMenuCanvas(true);
        }

        private void ActivateMenuCanvas(bool isActive)
        {
            innerCanvas.enabled = isActive;
        }
    } 
}