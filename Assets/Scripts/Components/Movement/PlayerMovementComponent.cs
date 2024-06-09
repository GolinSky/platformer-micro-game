using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Entities.PositionProvider;
using Mario.Services;
using Platformer.Mechanics;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public class PlayerMovementComponent: Component<MovementModel>, ITickable, IMovementCommand
    {
        private const float JumpSensitivity = 0.1f;
        
        private readonly IInputService inputService;
        private MovementComponent movementComponent;
        private bool isBlocked;

        public PlayerMovementComponent(IModel rootModel, IInputService inputService, PositionProvider positionProvider) : base(rootModel)
        {
            this.inputService = inputService;
            movementComponent = new MovementComponent(rootModel, positionProvider);
        }

        public void Tick()
        {
            if (!isBlocked)
            {
                if (inputService.HasDirectionInput)
                {
                    Model.Direction = inputService.Direction;

                    if (movementComponent.JumpState == JumpState.Grounded && inputService.Direction.y > JumpSensitivity)
                    {
                        movementComponent.SetJumpState(JumpState.PrepareToJump);
                        Model.InvokeJumpedEvent();
                    }
                    else if (inputService.Direction.y <= JumpSensitivity)
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
            else
            {
                Model.Direction = Vector2.zero;
                Model.Velocity = Vector2.zero;
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

        public void SpeedUp(float speedUpDuration)
        {
            movementComponent.SpeedUp(speedUpDuration);
        }

        public void TeleportToVictoryPosition()
        {
            movementComponent.TeleportToVictoryPosition();
        }
    }
}