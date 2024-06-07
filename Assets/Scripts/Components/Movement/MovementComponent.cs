using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Services;
using Platformer.Mechanics;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public class MovementComponent: Component<MovementModel>, ITickable
    {
        private readonly IInputService inputService;

        private Vector2 direction;
        private JumpState jumpState;
        private bool isStopJumping;
        private bool isJumping;

        public MovementComponent(IModel rootModel, IInputService inputService) : base(rootModel)
        {
            this.inputService = inputService;
        }
        
        public void Bounce(float value)
        {
            Model.SetVelocityOnAxisY(value);
        }
        
        public void Teleport(Vector3 position)
        {
            // body.position = position;
            // velocity *= 0;
            // body.velocity *= 0;

            Model.Velocity = Vector3.zero;
            //update view rigidbody
        }

        public void Tick()
        {
            if (inputService.HasDirectionInput)
            {
                Model.Direction = inputService.Direction;

                if (jumpState == JumpState.Grounded && inputService.Direction.y > 0.1f)
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (inputService.Direction.y <= 0.1f)
                {
                    isStopJumping = true;
                }
            }
            else
            {
                Model.Direction = Vector2.zero;
                isStopJumping = true;
            }
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
    }
}