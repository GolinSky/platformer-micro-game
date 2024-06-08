using Mario.Entities.Base;
using UnityEngine;

namespace Mario.Entities.Camera
{
    public class CameraView : View<ICameraModelObserver>
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera;

        protected override void OnInitialize()
        {
            UpdateTarget();
            Model.OnTargetChanged += UpdateTarget;
        }
        
        protected override void OnDispose()
        {
            Model.OnTargetChanged -= UpdateTarget;
        }

        private void UpdateTarget()
        {
            virtualCamera.Follow = Model.TargetTransform;
            virtualCamera.LookAt = Model.TargetTransform;
        }
    }
}