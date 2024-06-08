using LightWeightFramework.Model;
using Mario.Components.Health;
using Mario.Entities.Player;
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
        
        
        public override void Initialize()
        {
            healthModelObserver = PlayerModelObserver.GetModelObserver<IHealthModelObserver>();
            UpdateRespawnInformation();
            
            healthModelObserver.OnRespawn += UpdateRespawnInformation;
            UpdateDistanceText();
        }

        public override void LateDispose()
        {
            healthModelObserver.OnRespawn -= UpdateRespawnInformation;
        }
        
        private void UpdateRespawnInformation()
        {
            respawnText.text = healthModelObserver.RespawnAmount.ToString();
        }

        private void UpdateDistanceText()
        {
            distanceText.text = PlayerModelObserver.DistanceFromSpawnPoint.ToString();
        }

        protected override void OnUpdate()
        {
            UpdateDistanceText();
        }
    }
}