using System;
using UnityEngine;
using LightWeightFramework.Model;

namespace Mario.Entities.Camera
{
    public interface ICameraModelObserver : IModelObserver
    {
        event Action OnTargetChanged; 
        Transform TargetTransform { get; }
    }

    [CreateAssetMenu(fileName = "CameraModel", menuName = "Model/CameraModel")]
    public class CameraModel : Model, ICameraModelObserver
    {
        public event Action OnTargetChanged;

        private Transform playerTransform;


        public Transform TargetTransform
        {
            get => playerTransform;
            set
            {
                playerTransform = value;
                OnTargetChanged?.Invoke();
            }
        }
    }
}