using LightWeightFramework.Controller;
using Mario.Components.Health;
using Mario.Components.Movement;
using Utilities.ScriptUtils.Time;
using Zenject;

namespace Mario.Entities.Player
{
    public class PlayerController : Controller<PlayerModel>, IInitializable, ILateDisposable, ITickable
    {
        private readonly IMovementCommand movementCommand;
        private readonly IHealthCommand healthCommand;
        private readonly IHealthModelObserver healthModelObserver;
        private readonly ITimer rebornTimer;
        private bool canReborn;
        
        public PlayerController(PlayerModel model, IMovementCommand movementCommand, IHealthCommand healthCommand) : base(model)
        {
            this.movementCommand = movementCommand;
            this.healthCommand = healthCommand;
            healthModelObserver = model.GetModelObserver<IHealthModelObserver>();
            rebornTimer = TimerFactory.ConstructTimer(Model.RebornDelay);
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
            movementCommand.Bounce(Model.BounceForce);
        }

        private void OnDie()
        {
            canReborn = true;
            rebornTimer.StartTimer();
            movementCommand.Block(true);
        }

        public void Tick()
        {
            if (canReborn && rebornTimer.IsComplete)
            {
                canReborn = false;
                movementCommand.MoveToStartPosition();
                healthCommand.Reborn();
                movementCommand.Block(false);
            }
        }
    }
}