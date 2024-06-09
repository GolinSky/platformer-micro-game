using System;
using LightWeightFramework.Components.Service;
using Mario.Entities.Ui.Base;
using Mario.Services.SaveData;
using Zenject;

namespace Mario.Services.TokenService
{
    public interface ITokenService: IService
    {
        event Action OnCollected;
        int TokenAmount { get; }
        void Collect();
    }
    public class TokenService : Service, ITokenService, IInitializable, ILateDisposable, IGameObserver
    {
        private const int TokenDelta = 10;
        private const float PauseDuration = 3f;
        public event Action OnCollected;
        
        private readonly ISaveDataService saveDataService;
        private readonly ICoreService coreService;
        private readonly IUiService uiService;
        private int previousTokenAmount;

        public int TokenAmount { get; private set; }

        private string SaveDataKey => SaveDataTypes.Tokens.ToString();

        public TokenService(ISaveDataService saveDataService, ICoreService coreService, IUiService uiService)
        {
            this.saveDataService = saveDataService;
            this.coreService = coreService;
            this.uiService = uiService;

            if (saveDataService.HasKey(SaveDataKey))
            {
                TokenDto tokenDto = saveDataService.Get<TokenDto>(SaveDataKey);
                TokenAmount = tokenDto.TokenAmount;
            }
            else
            {
                TokenAmount = default;    
            }

            previousTokenAmount = TokenAmount;
        }

        public void Collect()
        {
            TokenAmount++;
            OnCollected?.Invoke();
            if (TokenAmount - previousTokenAmount == TokenDelta)
            {
                coreService.BlockGameFlow(PauseDuration);
                uiService.Show(UiType.Congratulations);
            }
        }

        public void Initialize()
        {
            coreService.AddObserver(this);
        }

        public void LateDispose()
        {
            coreService.RemoveObserver(this);
        }

        public void Update(GameState state)
        {
            if (state == GameState.Exit)
            {
                TokenDto tokenDto = new TokenDto(SaveDataKey, TokenAmount);
                saveDataService.Save(tokenDto);
            }
        }
    }
}