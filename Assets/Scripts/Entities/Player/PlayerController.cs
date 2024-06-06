using LightWeightFramework.Controller;

namespace Entities.Player
{
    public class PlayerController : Controller<PlayerModel>
    {
        public PlayerController(PlayerModel model) : base(model)
        {
        }
    }
}