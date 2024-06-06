using LightWeightFramework.Controller;

namespace Mario.Entities.Player
{
    public class PlayerController : Controller<PlayerModel>
    {
        public PlayerController(PlayerModel model) : base(model)
        {
        }
    }
}