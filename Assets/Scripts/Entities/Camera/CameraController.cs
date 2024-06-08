using LightWeightFramework.Controller;
using Mario.Components.Health;
using Mario.Components.Movement;
using Mario.Entities.Player;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Camera
{
    public class CameraController : Controller<CameraModel>, IInitializable, ILateDisposable
    {
        private readonly LazyInject<IPlayerModelObserver> playerModelObserver;
        private IHealthModelObserver healthModelObserver;
        private Transform target;

        public CameraController(CameraModel model, LazyInject<IPlayerModelObserver> playerModelObserver) : base(model)
        {
            this.playerModelObserver = playerModelObserver;
        }

        public void Initialize()
        {
            target = playerModelObserver.Value.GetModelObserver<IMovementModelObserver>().Transform;
            healthModelObserver = playerModelObserver.Value.GetModelObserver<IHealthModelObserver>();
            SetTarget();
            healthModelObserver.OnDied += ReleaseTarget;
            healthModelObserver.OnRespawn += SetTarget;
        }
        
        public void LateDispose()
        {
            healthModelObserver.OnDied -= ReleaseTarget;
            healthModelObserver.OnRespawn -= SetTarget;
        }

        private void SetTarget()
        {
            Model.TargetTransform = target;
        }

        private void ReleaseTarget()
        {
            Model.TargetTransform = null;
        }
    }
}