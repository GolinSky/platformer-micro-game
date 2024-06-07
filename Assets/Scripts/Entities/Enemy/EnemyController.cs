using LightWeightFramework.Command;
using LightWeightFramework.Controller;
using Mario.Components.Health;
using Zenject;

namespace Mario.Entities.Enemy
{
    public interface IEnemyCommand : ICommand
    {
    }
    public class EnemyController : Controller<EnemyModel>, IEnemyCommand, IInitializable, ILateDisposable
    {
        private readonly IHealthModelObserver healthModelObserver;

        public EnemyController(EnemyModel model) : base(model)
        {
            healthModelObserver = model.GetModelObserver<IHealthModelObserver>();
        }
        
        public void Initialize()
        {
            healthModelObserver.OnDied += OnDie;
        }
        
        public void LateDispose()
        {
            healthModelObserver.OnDied -= OnDie;
        }

        private void OnDie()
        {
            Model.InvokeFallBack();
        }
    }
}