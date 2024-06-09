using System;
using LightWeightFramework.Components.Service;

namespace Mario.Services.TokenService
{
    public interface ITokenService: IService
    {
        event Action OnCollected;
        int TokenAmount { get; }
        void Collect();
    }
    public class TokenService : Service, ITokenService
    {
        public event Action OnCollected;
        public int TokenAmount { get; private set; }

        public TokenService()
        {
            TokenAmount = default;
        }


        public void Collect()
        {
            TokenAmount++;
            OnCollected?.Invoke();
        }
    }
}