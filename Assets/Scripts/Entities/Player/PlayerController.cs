using System;
using LightWeightFramework.Command;
using LightWeightFramework.Controller;
using Mario.Components.Health;
using Mario.Components.Movement;
using Mario.Services;
using Utilities.ScriptUtils.Time;
using Zenject;

namespace Mario.Entities.Player
{
    public interface IPlayerCommand : ICommand
    {
        void SpeedUp();
        void SpawnToVictory();
    }
    public class PlayerController : Controller<PlayerModel>, IInitializable, ILateDisposable, ITickable, IGameObserver, IPlayerCommand
    {
        private readonly IMovementCommand movementCommand;
        private readonly IHealthCommand healthCommand;
        private readonly ICoreService coreService;
        private readonly IHealthModelObserver healthModelObserver;
        private readonly ITimer rebornTimer;
        private bool canReborn;
        
        public PlayerController(
            PlayerModel model,
            IMovementCommand movementCommand,
            IHealthCommand healthCommand,
            ICoreService coreService) : base(model)
        {
            this.movementCommand = movementCommand;
            this.healthCommand = healthCommand;
            this.coreService = coreService;
            healthModelObserver = model.GetModelObserver<IHealthModelObserver>();
            rebornTimer = TimerFactory.ConstructTimer(Model.RebornDelay);
        }

        public void Initialize()
        {
            healthModelObserver.OnDied += OnDie;
            healthModelObserver.OnApplyDamage += OnDamageApplied;
            coreService.AddObserver(this);
        }
        
        public void LateDispose()
        {
            healthModelObserver.OnDied -= OnDie;
            healthModelObserver.OnApplyDamage -= OnDamageApplied;
            coreService.RemoveObserver(this);
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

        public void Update(GameState state)
        {
            switch (state)
            {
                case GameState.Play:
                    movementCommand.Block(false);
                    break;
                case GameState.Pause:
                    movementCommand.Block(true);
                    break;
                case GameState.Win:
                {
                    movementCommand.Block(true);
                    Model.InvokeWinEvent();
                    break;
                }
            }
        }

        public void SpeedUp()
        {
            movementCommand.SpeedUp(Model.SpeedUpDuration);
        }

        public void SpawnToVictory()
        {
            movementCommand.TeleportToVictoryPosition();
        }
    }
}