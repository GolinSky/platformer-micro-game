using Mario.Entities.Base;
using Mario.Entities.Player;
using UnityEngine;

namespace Mario.Entities.Camera
{
    public class CameraView : View<ICameraModelObserver>
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera;

        protected override void OnInitialize()
        {
            virtualCamera.Follow = Model.PlayerTransform;
        }

        protected override void OnDispose()
        {
        }
    }
}