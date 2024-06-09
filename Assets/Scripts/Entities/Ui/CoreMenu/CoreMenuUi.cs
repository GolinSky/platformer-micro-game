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
        
        [Inject]
        private IAudioCommand AudioCommand { get; } 
        
        [Inject]
        private IAudioService AudioService { get; }
        
        public override void Initialize()
        {
            base.Initialize();
            soundToggle.isOn = !AudioService.SoundSettings.IsMute;
            musicToggle.isOn = !AudioService.MusicSettings.IsMute;
            
            openButton.onClick.AddListener(OpenMenu);
            closeButton.onClick.AddListener(CloseMenu);
            exitButton.onClick.AddListener(CoreGameCommand.Exit);
            openButton.onClick.AddListener(CoreGameCommand.EnterMenu);
            closeButton.onClick.AddListener(CoreGameCommand.ExitMenu);
            soundToggle.onValueChanged.AddListener(AudioCommand.MuteSound);
            musicToggle.onValueChanged.AddListener(AudioCommand.MuteMusic);
        }

        public override void LateDispose()
        {
            base.LateDispose();
            openButton.onClick.RemoveListener(OpenMenu);
            closeButton.onClick.RemoveListener(CloseMenu);
            exitButton.onClick.RemoveListener(CoreGameCommand.Exit);
            openButton.onClick.RemoveListener(CoreGameCommand.EnterMenu);
            closeButton.onClick.RemoveListener(CoreGameCommand.ExitMenu);
            soundToggle.onValueChanged.RemoveListener(AudioCommand.MuteSound);
            musicToggle.onValueChanged.RemoveListener(AudioCommand.MuteMusic);
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