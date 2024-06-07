using LightWeightFramework.Command;
using LightWeightFramework.Model;
using Mario.Components.Base;
using Platformer.Mechanics;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public interface IMovementCommand: ICommand
    {
        void Bounce(float value);
        void MoveToStartPosition();
        void Block(bool isBlocked);
    }

    public class MovementComponent: Component<MovementModel>, ITickable, IMovementCommand
    {
        private Vector2 direction;
        private JumpState jumpState;
        private bool isStopJumping;
        private bool isJumping;
        private bool isBlocked;

        public MovementComponent(IModel rootModel) : base(rootModel)
        {
        }

        public JumpState JumpState => jumpState;

        public void Bounce(float value)
        {
            Model.SetVelocityOnAxisY(value);
        }
        
        public void MoveToStartPosition()
        {
            Model.Velocity = Vector3.zero;
            Model.MoveToStartPosition();
        }

        public void Block(bool isBlocked)
        {
            //fix this
        }

        public void Tick()
        {
            UpdateJumpState();
            ComputeVelocity();
        }

        private void UpdateJumpState()
        {
            isJumping = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    isJumping = true;
                    isStopJumping = false;
                    break;
                case JumpState.Jumping:
                    if (!Model.IsGrounded)
                    {
                        // Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (Model.IsGrounded)
                    {
                        // Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        private void ComputeVelocity()
        {
            Model.TargetVelocity = Vector2.zero;

            if (isJumping && Model.IsGrounded)
            {
                Model.SetVelocityOnAxisY(Model.JumpTakeOffSpeed * Model.JumpModifier);
                isJumping = false;
            }
            else if (isStopJumping)
            {
                isStopJumping = false;
                if (Model.Velocity.y > 0)
                {
                    Model.SetVelocityOnAxisY(Model.Velocity.y * Model.JumpDeceleration);
                }
            }

            Model.TargetVelocity = Model.Direction * Model.MaxHorizontalSpeed;
        }

        public void SetJumpState(JumpState jumpState)
        {
            this.jumpState = jumpState;
        }

        public void StopJumping()
        {
            isStopJumping = true;
        }
    }
}