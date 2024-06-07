using LightWeightFramework.Controller;
using Mario.Components.Health;
using Mario.Components.Movement;
using Zenject;

namespace Mario.Entities.Player
{
    public class PlayerController : Controller<PlayerModel>, IInitializable, ILateDisposable
    {
        private readonly IMovementCommand movementCommand;
        private readonly IHealthCommand healthCommand;
        private readonly IHealthModelObserver healthModelObserver;
        
        public PlayerController(PlayerModel model, IMovementCommand movementCommand, IHealthCommand healthCommand) : base(model)
        {
            this.movementCommand = movementCommand;
            this.healthCommand = healthCommand;
            healthModelObserver = model.GetModelObserver<IHealthModelObserver>();
        }

        public void Initialize()
        {
            healthModelObserver.OnDied += OnDie;
            healthModelObserver.OnApplyDamage += OnDamageApplied;
        }
        
        public void LateDispose()
        {
            healthModelObserver.OnDied -= OnDie;
            healthModelObserver.OnApplyDamage -= OnDamageApplied;
        }

        private void OnDamageApplied()
        {
            movementCommand.Bounce(2f);
        }

        private void OnDie()
        {
            movementCommand.MoveToStartPosition();
            healthCommand.Reborn();
        }
    }
}