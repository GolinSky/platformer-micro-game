using LightWeightFramework.Controller;
using Mario.Services;
using Zenject;

namespace Mario.Entities.Player
{
    public class PlayerController : Controller<PlayerModel>, ITickable
    {
        private readonly IInputService inputService;

        public PlayerController(PlayerModel model, IInputService inputService) : base(model)
        {
            this.inputService = inputService;
        }

        public void Tick()
        {
           // Model.CurrentDirection = inputService.Direction;
        }
    }
}