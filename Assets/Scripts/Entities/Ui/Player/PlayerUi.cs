using Mario.Components.Health;
using Mario.Configuration;
using Mario.Entities.Player;
using Mario.Services.TokenService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Ui.Player
{
    public class PlayerUi: Base.Ui
    {
        [SerializeField] private TextMeshProUGUI respawnText;
        [SerializeField] private TextMeshProUGUI diamondText;
        [SerializeField] private TextMeshProUGUI distanceText;

        private IHealthModelObserver healthModelObserver;
        
        [Inject]
        public IPlayerModelObserver PlayerModelObserver { get; }
        
        [Inject]
        public ITokenService TokenService { get; } // bad code
        
        public override void Initialize()
        {
            healthModelObserver = PlayerModelObserver.GetModelObserver<IHealthModelObserver>();
            UpdateRespawnInformation();
            UpdateDistanceText();
            UpdateTokenText();
            healthModelObserver.OnRespawn += UpdateRespawnInformation;
            TokenService.OnCollected += UpdateTokenText;
        }


        public override void LateDispose()
        {
            healthModelObserver.OnRespawn -= UpdateRespawnInformation;
            TokenService.OnCollected -= UpdateTokenText;
        }
        
        private void UpdateTokenText()
        {
            diamondText.text = TokenService.TokenAmount.ToString(StringConfiguration.CultureInfo);
        }
        
        private void UpdateRespawnInformation()
        {
            respawnText.text = healthModelObserver.RespawnAmount.ToString(StringConfiguration.CultureInfo);
        }

        private void UpdateDistanceText()
        {
            distanceText.text = Mathf.RoundToInt(PlayerModelObserver.DistanceFromSpawnPoint).ToString(StringConfiguration.CultureInfo);
        }

        protected override void OnUpdate()
        {
            UpdateDistanceText();
        }
    }
}