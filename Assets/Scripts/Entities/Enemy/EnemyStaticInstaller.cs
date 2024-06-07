using Mario.Components.Health;
using Mario.Components.Movement;
using Mario.Zenject.Extensions;
using Mario.Zenject.GameObjectInstallers;

namespace Mario.Entities.Enemy
{
    public class EnemyStaticInstaller : StaticViewInstaller<EnemyController, EnemyModel>
    {
        protected override void BindParameters()
        {
            base.BindParameters();
            Container.BindEntity(View.Transform.position); // use view position as start one - instead of dynamic start position data
        }

        protected override void BindComponents()
        {
            base.BindComponents();
            Container.BindInterfaces<HealthComponent>();
            Container.BindInterfaces<MovementComponent>();
        }
    }
}