using System;
using LightWeightFramework.Model;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public interface IMovementModelObserver : IModelObserver
    {
        Vector3 StartPosition { get; }
    }

    [Serializable]
    public class MovementModel : InnerModel, IMovementModelObserver
    {
        [Inject]
        public Vector3 StartPosition { get; }
    }
}