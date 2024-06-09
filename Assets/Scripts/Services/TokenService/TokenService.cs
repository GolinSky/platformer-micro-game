using System;
using LightWeightFramework.Components.Service;
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
        public event Action OnCollected;
        
        private readonly ISaveDataService saveDataService;
        private readonly ICoreService coreService;

        public int TokenAmount { get; private set; }

        private string SaveDataKey => nameof(TokenService);

        public TokenService(ISaveDataService saveDataService, ICoreService coreService)
        {
            this.saveDataService = saveDataService;
            this.coreService = coreService;

            if (saveDataService.HasKey(SaveDataKey))
            {
                TokenDto tokenDto = saveDataService.Get<TokenDto>(SaveDataKey);
                TokenAmount = tokenDto.TokenAmount;
            }
            else
            {
                TokenAmount = default;    
            }
        }

        public void Collect()
        {
            TokenAmount++;
            OnCollected?.Invoke();
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