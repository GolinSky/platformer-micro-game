using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Services;
using Platformer.Mechanics;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public class MovementComponent: Component<MovementModel>, ITickable, IFixedTickable
    {
        private readonly IInputService inputService;

        private Vector2 direction;
        private JumpState jumpState;
        private bool stopJump;
        public bool jump;

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

                if (jumpState == JumpState.Grounded && inputService.Direction.y > 0)
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (inputService.Direction.y <= 0f)
                {
                    stopJump = true;
                }
            }
            else
            {
                Model.Direction = Vector2.zero;
                stopJump = true;

            }
            UpdateJumpState();

            ComputeVelocity();
        }
        
        public void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
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

            if (jump && Model.IsGrounded)
            {
                Model.SetVelocityOnAxisY(Model.JumpTakeOffSpeed * Model.JumpModifier);
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (Model.Velocity.y > 0)
                {
                    Model.SetVelocityOnAxisY(Model.Velocity.y * Model.JumpDeceleration);
                }
            }

            // if (move.x > 0.01f)
            //     spriteRenderer.flipX = false;
            // else if (move.x < -0.01f)
            //     spriteRenderer.flipX = true;

            // animator.SetBool("grounded", IsGrounded);
            // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            Model.TargetVelocity = Model.Direction * Model.MaxHorizontalSpeed;
        }

        public void FixedTick()
        {
           
        }
    }
}