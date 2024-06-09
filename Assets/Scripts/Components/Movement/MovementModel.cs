using System;
using LightWeightFramework.Model;
using Mario.Zenject.GameObjectInstallers;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public interface IMovementModelObserver : IModelObserver
    {
        event Action<Vector3> OnPositionChanged;
        event Action OnJumped;
        event Action OnMoveToStartPosition;
        
        Transform Transform { get; }
        Vector3 StartPosition { get; }
        Vector2 Velocity { get; set; }// todo: refactor
        Vector2 TargetVelocity { get; }
        Vector2 Direction { get; }
        Vector2 CurrentPosition { get; }
        
        float MinGroundNormalY { get; }
        float GravityModifier { get; }
        float HorizontalSpeed { get; }
        bool IsGrounded { get; set; } // todo: refactor
        
        void SetVelocityOnAxisX(float value);
        void SetVelocityOnAxisY(float value);

        void ResetVelocity();
    }

    [Serializable]
    public class MovementModel : InnerModel, IMovementModelObserver
    {
        private const float DefaultSpeedBoost = 1f;
        
        public event Action<Vector3> OnPositionChanged;
        public event Action OnJumped;
        public event Action OnMoveToStartPosition;
        

        [SerializeField] private float speedBoost;
        [field: SerializeField] public float MinGroundNormalY { get; private set; }
        [field: SerializeField] public float GravityModifier { get; private set; }
        [field: SerializeField] public float JumpModifier { get; private set; }
        [field: SerializeField] public float JumpDeceleration { get; private set; }
        [field: SerializeField] public float JumpTakeOffSpeed { get; private set; }
        [field: SerializeField] public float MaxHorizontalSpeed { get; private set; }
        public float HorizontalSpeed => MaxHorizontalSpeed * speedBoostModifier;

        
        [Inject(Id = EntityBindType.ViewTransform)]
        public LazyInject<Transform> ViewTransform { get; }

        [Inject]
        public Vector3 StartPosition { get; }

        public Transform Transform => ViewTransform.Value;
        
        public Vector2 Velocity { get; set; }
        
        public Vector2 Direction { get; set; }
        
        public Vector2 TargetVelocity { get; set; }

        public Vector2 CurrentPosition => ViewTransform.Value.position;
        
        public bool IsGrounded { get; set; }

        private float speedBoostModifier = DefaultSpeedBoost;

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

        public void MoveTo(Vector3 position)
        {
            OnPositionChanged?.Invoke(position);
        }

        public void InvokeJumpedEvent()
        {
            OnJumped?.Invoke();
        }

        public void ActivateSpeedBoostModifier()
        {
            speedBoostModifier = speedBoost;
        }
        
        public void ResetSpeedBoostModifier()
        {
            speedBoostModifier = DefaultSpeedBoost;
        }
    }
}