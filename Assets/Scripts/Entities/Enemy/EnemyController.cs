using LightWeightFramework.Command;
using LightWeightFramework.Controller;
using Mario.Components.Health;
using Mario.Components.Movement;
using Platformer.Mechanics;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Enemy
{
    public interface IEnemyCommand : ICommand
    {
    }
    public class EnemyController : Controller<EnemyModel>, IEnemyCommand, IInitializable, ILateDisposable, ITickable
    {
        private readonly IHealthModelObserver healthModel;

        [InjectOptional]
        private PatrolPath PatrolPath { get; }
        private PatrolPath.Mover mover;
        private Vector2 direction;
        
        public EnemyController(EnemyModel model) : base(model)
        {
            healthModel = model.GetModelObserver<IHealthModelObserver>();
        }
        
        public void Initialize()
        {
            healthModel.OnDied += OnDie;
            if (PatrolPath != null)
            {
                mover = PatrolPath.CreateMover(Model.MovementModel.HorizontalSpeed * 0.5f);
            }
        }
        
        public void LateDispose()
        {
            healthModel.OnDied -= OnDie;
        }

        private void OnDie()
        {
            Model.InvokeFallBack();
        }

        public void Tick()
        {
            if (PatrolPath != null)
            {
                direction.x = Mathf.Clamp(mover.Position.x - Model.MovementModel.CurrentPosition.x, -1, 1);
                direction.y = Model.MovementModel.Direction.y;
                Model.MovementModel.Direction = direction;
            }
        }
    }
}