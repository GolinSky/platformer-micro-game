using UnityEngine;
using LightWeightFramework.Model;

namespace Mario.Entities.Camera
{
    public interface ICameraModelObserver : IModelObserver
    {
        Transform PlayerTransform { get; }
    }

    [CreateAssetMenu(fileName = "CameraModel", menuName = "Model/CameraModel")]
    public class CameraModel : Model, ICameraModelObserver
    {
        public Transform PlayerTransform { get; set; }
    }
}