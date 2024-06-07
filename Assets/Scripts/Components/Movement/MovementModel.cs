using System;
using LightWeightFramework.Model;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public interface IMovementModelObserver : IModelObserver
    {
        event Action OnMoveToStartPosition;
        Vector3 StartPosition { get; }
        Vector2 Velocity { get; set; }// todo: refactor
        Vector2 TargetVelocity { get; }
        bool IsGrounded { get; set; } // todo: refactor
        float MinGroundNormalY { get; }
        float GravityModifier { get; }
        float MaxHorizontalSpeed { get; }
        Vector2 Direction { get; }
        void SetVelocityOnAxisX(float value);
        void SetVelocityOnAxisY(float value);

        void ResetVelocity();
    }

    [Serializable]
    public class MovementModel : InnerModel, IMovementModelObserver
    {
        [field: SerializeField] public float MinGroundNormalY { get; private set; }
        [field: SerializeField] public float GravityModifier { get; private set; }
        [field: SerializeField] public float JumpModifier { get; private set; }
        [field: SerializeField] public float JumpDeceleration { get; private set; }
        [field: SerializeField] public float JumpTakeOffSpeed { get; private set; }

        [field: SerializeField] public float MaxHorizontalSpeed { get; private set; }

        public event Action OnMoveToStartPosition;

        [Inject]
        public Vector3 StartPosition { get; }
        
        public Vector2 Velocity { get; set; }
        
        public Vector2 Direction { get; set; }
        
        public Vector2 TargetVelocity { get; set; }
        
        public bool IsGrounded { get; set; }
        

        public void SetVelocityOnAxisY(float value)
        {
            Vector2 vector3 = Velocity;
            vector3.y = value;
            Velocity = vector3;
        }

        public void ResetVelocity()
        {
            SetVelocityOnAxisX(0);
            SetVelocityOnAxisY(Mathf.Min(Velocity.y, 0));
        }

        public void SetVelocityOnAxisX(float value)
        {
            Vector2 vector3 = Velocity;
            vector3.x = value;
            Velocity = vector3;
        }

        public void MoveToStartPosition()
        {
            OnMoveToStartPosition?.Invoke();
        }
    }
}