using LightWeightFramework.Model;
using Mario.Components.Base;
using Mario.Entities.PositionProvider;
using Mario.Services;
using Platformer.Mechanics;
using UnityEngine;
using Utilities.ScriptUtils.Math;
using Utilities.ScriptUtils.Time;
using Zenject;

namespace Mario.Components.Movement
{
    public class PlayerMovementComponent: Component<MovementModel>, ITickable, IMovementCommand, IInitializable, ILateDisposable
    {
        private const float JumpSensitivity = 0.1f;
        
        private readonly IInputService inputService;
        private readonly IAudioService audioService;
        private readonly ITimer blockTimer;

        private readonly MovementComponent movementComponent;
        private bool isBlocked;
        private bool targetBlockValue;
        
        public PlayerMovementComponent(
            IModel rootModel,
            IInputService inputService,
            PositionProvider positionProvider,
            IAudioService audioService) : base(rootModel)
        {
            this.inputService = inputService;
            this.audioService = audioService;
            movementComponent = new MovementComponent(rootModel, positionProvider);
            blockTimer = TimerFactory.ConstructTimer();
        }

        public void Initialize()
        {
            audioService.SetSpeed(Model.SpeedBoostModifier);
            Model.OnSpeedChanged += HandleSpeed;
        }
        
        public void LateDispose()
        {
            Model.OnSpeedChanged -= HandleSpeed;
        }
        
        private void HandleSpeed()
        {
            audioService.SetSpeed(Model.SpeedBoostModifier);
        }
        
        public void Tick()
        {
            if (targetBlockValue != isBlocked)
            {
                if (blockTimer.IsComplete)
                {
                    isBlocked = targetBlockValue;
                }
            }
            
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

        public void Block(bool isBlocked, float delay)
        {
            if (delay.IsEqual(0))
            {
                targetBlockValue = isBlocked;
                this.isBlocked = isBlocked;
            }
            else
            {
                targetBlockValue = isBlocked;
                blockTimer.ChangeDelay(delay);
                blockTimer.StartTimer();
            }
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