using LightWeightFramework.Controller;
using Mario.Components.Movement;
using Mario.Entities.Player;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Camera
{
    public class CameraController : Controller<CameraModel>, IInitializable
    {
        private readonly LazyInject<IPlayerModelObserver> playerModelObserver;
        private Transform target;

        public CameraController(CameraModel model, LazyInject<IPlayerModelObserver> playerModelObserver) : base(model)
        {
            this.playerModelObserver = playerModelObserver;
        }

        public void Initialize()
        {
            target = playerModelObserver.Value.GetModelObserver<IMovementModelObserver>().Transform;
            Model.PlayerTransform = target;
        }
    }
}