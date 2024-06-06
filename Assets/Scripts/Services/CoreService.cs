using LightWeightFramework.Components.Service;
using Mario.Entities.Player;
using Zenject;

namespace Mario.Services
{
    public class CoreService: Service, IInitializable
    {
        private readonly PlayerFacade playerFacade;
        private PlayerView playerView;

        public CoreService(PlayerFacade playerFacade)
        {
            this.playerFacade = playerFacade;
        }

        public void Initialize()
        {
            playerView = playerFacade.Create();
        }
    }
}