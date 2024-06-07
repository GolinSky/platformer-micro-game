using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Services;
using Platformer.Mechanics;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public class PlayerMovementComponent: Component<MovementModel>, ITickable, IMovementCommand
    {
        private readonly IInputService inputService;
        private MovementComponent movementComponent;
        private bool isBlocked;

        public PlayerMovementComponent(IModel rootModel, IInputService inputService) : base(rootModel)
        {
            this.inputService = inputService;
            movementComponent = new MovementComponent(rootModel);
        }

        public void Tick()
        {
            if (!isBlocked)
            {
                if (inputService.HasDirectionInput)
                {
                    Model.Direction = inputService.Direction;

                    if (movementComponent.JumpState == JumpState.Grounded && inputService.Direction.y > 0.1f)
                    {
                        movementComponent.SetJumpState(JumpState.PrepareToJump);
                        Model.InvokeJumpedEvent();
                    }
                    else if (inputService.Direction.y <= 0.1f)
                    {
                        movementComponent.StopJumping();
                    }
                }
                else
                {
                    Model.Direction = Vector2.zero;
                    movementComponent.StopJumping();
                }
            }
            
            movementComponent.Tick();
        }

        public void Bounce(float value)
        {
            movementComponent.Bounce(value);
        }

        public void MoveToStartPosition()
        {
            movementComponent.MoveToStartPosition();
        }

        public void Block(bool isBlocked)
        {
            this.isBlocked = isBlocked;
        }
    }
}