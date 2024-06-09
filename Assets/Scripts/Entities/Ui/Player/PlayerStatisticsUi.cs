using Mario.Configuration;
using Mario.Entities.Player;
using Mario.Services.SaveData;
using Mario.Services.TokenService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Ui.Player
{
    public class PlayerStatisticsUi : Base.Ui
    {
        [SerializeField] private TextMeshProUGUI respawnText;
        [SerializeField] private TextMeshProUGUI diamondText;
        [SerializeField] private TextMeshProUGUI distanceText;


        [Inject]
        private SaveDataService SaveDataService { get; }
        
        public override void Initialize()
        {
            base.Initialize();
            string playerKey = SaveDataTypes.Player.ToString();
            string tokenKey = SaveDataTypes.Tokens.ToString();

            if (SaveDataService.HasKey(playerKey))
            {
                PlayerDto playerDto = SaveDataService.Get<PlayerDto>(playerKey);
                respawnText.text = playerDto.RespawnCount.ToString(StringConfiguration.CultureInfo);
                distanceText.text = Mathf.RoundToInt(playerDto.Distance).ToString(StringConfiguration.CultureInfo);
            }

            if (SaveDataService.HasKey(tokenKey))
            {
                TokenDto tokenDto = SaveDataService.Get<TokenDto>(tokenKey);
                diamondText.text = tokenDto.TokenAmount.ToString(StringConfiguration.CultureInfo);
            }
        }
    }
}